public class Auth0Data
{
    
    public const string DOMAIN = "dev-05j84tecmi7hx6en.eu.auth0.com";
    public const string CLIENT_ID = "aPDW8t4xJC8D3wDnyn8BCC9lpAilc9jq";
    public const string CLIENT_SECRET = "M9EXNTVw9UQ7RKNURtk6YGg4is3ii_eZR7rG5XbSRLVu9zUWQb3X4xVrg28mZvN8";
    public const string REDIRECTURI = "http://localhost:5012/callback";
    public const string AUDIENCE = "aPDW8t4xJC8D3wDnyn8BCC9lpAilc9jq";

    public const string REDIRECTURI2 = "http://localhost:5012/callback";
    public const string ROLES_URL = "https://hellth.com/claims/roles";

    public const string ADMIN_ID = "rol_UhqXdIfzGmE8Tmo5";
    public const string DOCTOR_ID = "rol_C94ccWTJEdVRWjud";
    public const string TECH_ID = "rol_uU7tHRYUzXNtJSFI";
    public const string NURSE_ID = "rol_EfcoakZhaTRGK3Pd";
    public const string PATIENT_ID = "rol_dCC2CEsyTxIiGql5";

    public static Dictionary<string, string> map = new Dictionary<string, string>
    {
        { "Admin", ADMIN_ID },
        { "Doctor", DOCTOR_ID },
        { "Technician", TECH_ID },
        { "Nurse", NURSE_ID }, 
        { "Patient", PATIENT_ID }
    };

}