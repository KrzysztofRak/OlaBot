using Chomikuj.API;

namespace Test.APP

{
    class Program
    {
        static void Main(string[] args)
        {
            ChomikujAPI chomikujAPI = new ChomikujAPI();
            chomikujAPI.Login("GameOverMan", "5991DSAwyipmbczK");
            chomikujAPI.CopyFolder("http://chomikuj.pl/hyper74/foto*2c+memy*2c+*c5*9bmieszne+i+r*c3*b3*c5*bcne/horrorki", "http://chomikuj.pl/GameOverMan");
        }
    }
}
