namespace FourMinator.BotLogic
{
    public class OpeningBook
    {
        private ITableGetter<ulong, byte> transpositionTable;
        private readonly int width;
        private readonly int height;
        private int depth;

        public OpeningBook(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.depth = -1;
            this.transpositionTable = null; // Empty opening book
        }

        public OpeningBook(int width, int height, int depth, ITableGetter<ulong, byte> transpositionTable)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;
            this.transpositionTable = transpositionTable;
        }

        private ITableGetter<ulong, byte> InitTranspositionTable<TPartialKey>(int logSize) where TPartialKey : IConvertible
        {
            return logSize switch
            {
                21 => new TranspositionTable<TPartialKey, byte>(21),
                22 => new TranspositionTable<TPartialKey, byte>(22),
                23 => new TranspositionTable<TPartialKey, byte>(23),
                24 => new TranspositionTable<TPartialKey, byte>(24),
                25 => new TranspositionTable<TPartialKey, byte>(25),
                26 => new TranspositionTable<TPartialKey, byte>(26),
                27 => new TranspositionTable<TPartialKey, byte>(27),
                _ => LogErrorAndReturnNull($"Unimplemented OpeningBook size: {logSize}")
            };
        }

        private ITableGetter<ulong, byte> InitTranspositionTable(int partialKeyBytes, int logSize)
        {
            return partialKeyBytes switch
            {
                1 => InitTranspositionTable<byte>(logSize),
                2 => InitTranspositionTable<ushort>(logSize),
                4 => InitTranspositionTable<uint>(logSize),
                _ => LogErrorAndReturnNull($"Invalid internal key size: {partialKeyBytes} bytes")
            };
        }

        private static ITableGetter<ulong, byte> LogErrorAndReturnNull(string message)
        {
            Console.Error.WriteLine(message);
            return null;
        }

        public void Load(string filename)
        {
            depth = -1;
            transpositionTable = null;

            using (var ifs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(ifs))
            {
                if (ifs.Length == 0)
                {
                    Console.Error.WriteLine($"Unable to load opening book: {filename}");
                    return;
                }

                Console.Error.Write($"Loading opening book from file: {filename}. ");

                byte _width = reader.ReadByte();
                if (_width != width)
                {
                    Console.Error.WriteLine($"Unable to load opening book: invalid width (found: {_width}, expected: {width})");
                    return;
                }

                byte _height = reader.ReadByte();
                if (_height != height)
                {
                    Console.Error.WriteLine($"Unable to load opening book: invalid height (found: {_height}, expected: {height})");
                    return;
                }

                byte _depth = reader.ReadByte();
                if (_depth > width * height)
                {
                    Console.Error.WriteLine($"Unable to load opening book: invalid depth (found: {_depth})");
                    return;
                }

                byte partialKeyBytes = reader.ReadByte();
                if (partialKeyBytes > 8)
                {
                    Console.Error.WriteLine($"Unable to load opening book: invalid internal key size (found: {partialKeyBytes})");
                    return;
                }

                byte valueBytes = reader.ReadByte();
                if (valueBytes != 1)
                {
                    Console.Error.WriteLine($"Unable to load opening book: invalid value size (found: {valueBytes}, expected: 1)");
                    return;
                }

                byte logSize = reader.ReadByte();
                if (logSize > 40)
                {
                    Console.Error.WriteLine($"Unable to load opening book: invalid log2(size) (found: {logSize})");
                    return;
                }

                transpositionTable = InitTranspositionTable(partialKeyBytes, logSize);
                if (transpositionTable == null)
                {
                    Console.Error.WriteLine("Unable to initialize opening book");
                    return;
                }

                byte[] keys = reader.ReadBytes(transpositionTable.GetSize() * partialKeyBytes);
                byte[] values = reader.ReadBytes(transpositionTable.GetSize() * valueBytes);

                if (keys.Length != transpositionTable.GetSize() * partialKeyBytes || values.Length != transpositionTable.GetSize() * valueBytes)
                {
                    Console.Error.WriteLine("Unable to load data from opening book");
                    return;
                }

                Buffer.BlockCopy(keys, 0, transpositionTable.GetKeys(), 0, keys.Length);
                Buffer.BlockCopy(values, 0, transpositionTable.GetValues(), 0, values.Length);

                depth = _depth; // Set depth in case of success
                Console.Error.WriteLine("done");
            }
        }

        public void Save(string outputFile)
        {
            using var fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write);
            using var writer = new BinaryWriter(fs);

            writer.Write((byte)width);
            writer.Write((byte)height);
            writer.Write((byte)depth);
            writer.Write((byte)transpositionTable.GetKeySize());
            writer.Write((byte)transpositionTable.GetValueSize());
            writer.Write((byte)Math.Log(transpositionTable.GetSize(), 2));

            WriteKeysAndValues(writer, transpositionTable);
        }

        private static void WriteKeysAndValues(BinaryWriter writer, ITableGetter<ulong, byte> table)
        {
            byte[] keyBuffer = new byte[table.GetSize() * table.GetKeySize()];
            byte[] valueBuffer = new byte[table.GetSize() * table.GetValueSize()];

            Buffer.BlockCopy(table.GetKeys(), 0, keyBuffer, 0, keyBuffer.Length);
            Buffer.BlockCopy(table.GetValues(), 0, valueBuffer, 0, valueBuffer.Length);

            writer.Write(keyBuffer);
            writer.Write(valueBuffer);
        }

        public int Get(Position position)
        {
            return position.GetMoveCount() > depth ? 0 : transpositionTable.GetBook(position.Key3());
        }
    }

}
