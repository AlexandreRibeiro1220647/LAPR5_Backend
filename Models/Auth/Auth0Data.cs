public class Auth0Data
{
    
    public const string DOMAIN = "dev-05j84tecmi7hx6en.eu.auth0.com";
    public const string CLIENT_ID = "qp7iwBhAwup1NdT0NAPswPBa3nvVfkvs";
    public const string CLIENT_SECRET = "b7M9jLQy4zadf97tTZ8uSX_u61v-GFbHTMdq5va-SmWVOK2rqhERPL0HXd9TPq-3";
    public const string REDIRECTURI = "http://localhost:5001/callback";
    public const string AUDIENCE = "https://healthcarelogin.com";

    public const string REDIRECTURI2 = "http://localhost:5001/callback/";
    public const string ROLES_URL = "http://dev-b2f7avjyddz6kpot.us.auth0.comroles";

    public const string ADMIN_ID = "rol_UhqXdIfzGmE8Tmo5";
    public const string DOCTOR_ID = "rol_C94ccWTJEdVRWjud";
    public const string TECH_ID = "";
    public const string NURSE_ID = "rol_EfcoakZhaTRGK3Pd";
    public const string PATIENT_ID = "rol_dCC2CEsyTxIiGql5";

    public static Dictionary<string, string> map = new Dictionary<string, string>
    {
        { "Admin", ADMIN_ID },
        { "Doctor", DOCTOR_ID },
        { "Nurse", NURSE_ID }, 
        { "Patient", PATIENT_ID },
        { "Technician", TECH_ID }
    };

}