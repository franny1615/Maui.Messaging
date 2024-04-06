namespace Messaging.Api.Repositories.LoginRepo;

public class LoginRepository : BaseRepository, ILoginRepository
{
    public string GetJwtToken()
    {
        // this is for giggles obviously
        return Auth.MintJWTForUser();
    }
}

