using Chomikuj.API.Repositories;

namespace Chomikuj.API
{
    public class ChomikujAPI : IChomikujAPI
    {
        private ChomikujRepository chomikujRepo = new ChomikujRepository();

        public bool Login(string username, string password)
        {
            return chomikujRepo.Login(username, password);
        }

        public void Logout()
        {
            chomikujRepo.Logout();
        }

        public void CopyFolder(string srcFolderUrl, string destFolderUrl)
        {
            chomikujRepo.SetRequestBasicData(srcFolderUrl);
            chomikujRepo.CopyFolder("293", "16177", "jakas dziwna ta nazwa");
        }

        public void CopyFolderWithChildren(string srcFolderUrl, string destFolderUrl)
        {

        }
    }
}
