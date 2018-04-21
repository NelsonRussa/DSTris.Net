using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DSTris.Model
{
    class Config
    {
        // Guardar as configurações gerais do jogo
        public class GameConfig
        {
            public string AssetsFolder { get; set; }
            public string FontName { get; set; }
            public string FullFontName { get { return $"{AssetsFolder}\\{FontName}"; } }
        }
        public GameConfig Game;


        // 
        public void Load()
        {
            // Ler ficheiro de configuração, caso não consiga, gera uma exceção
            var xmlDoc = new XmlDocument();
            if (!File.Exists("config.xml"))
                throw new FileNotFoundException("Ficheiro de configuração não encontrado!", "config.xml");
            xmlDoc.Load("config.xml");
            var xmlDocElem = xmlDoc.DocumentElement;


            // Ler configurações do jogo

            // Config/Game
            var nodeGame = xmlDocElem.SelectSingleNode("/config/game");
            Game = new GameConfig();
            Game.AssetsFolder = nodeGame.Attributes["assetsFolder"].Value;
            Game.FontName = nodeGame.Attributes["fontName"].Value;
            if (!File.Exists(Game.FullFontName))
                throw new FileNotFoundException("Ficheiro da fonte não encontrado!", Game.FontName);

        }
    }
}
