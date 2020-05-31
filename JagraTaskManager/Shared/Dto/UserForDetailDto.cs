using System;

namespace JagraTaskManager.Shared.Dto
{
    public class UserForDetailDto
    {
        //{
        //  "nameid": "a3bff119-da74-4f7c-8146-c3adb75227e9",
        //  "unique_name": "nerocui@outlook.com",
        //  "email": "nerocui@outlook.com",
        //  "given_name": "nero",
        //  "family_name": "cui",
        //  "nbf": 1588919316,
        //  "exp": 1589005716,
        //  "iat": 1588919316
        //}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime TimeOfRegister { get; set; }
    }
}
