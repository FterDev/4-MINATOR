using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.AuthServices.Middleware
{
    public class FireAuth
    {

        private readonly FirebaseApp _firebase;


        public FireAuth(FirebaseApp firebase)
        {
            _firebase = firebase;  
        }

        public async Task<FirebaseToken> Authorize(string token)
        {
            var auth = FirebaseAuth.GetAuth(_firebase);

            var res = auth.VerifyIdTokenAsync(token).Result;

            return res;
        }
    }
}
