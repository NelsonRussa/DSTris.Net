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
            public class BackgroundConfig
            {
                public string TextureName { get; set; }
                public Vector2f GameCoords { get; set; }
                public Vector2f NextBlockCoords { get; set; }
            }

            public string AssetsFolder { get; set; }
            public string FontName { get; set; }
            public BackgroundConfig Background;

            public GameConfig()
            {
                Background = new BackgroundConfig();
            }

        }
        public class MenuConfig
        {
            public class BackgroundConfig
            {
                public string TextureName { get; set; }
            }
            public BackgroundConfig Background;
        }
        public class BlockConfig
        {
            public string TextureName { get; set; }
            public List<Vector2i> Parts { get; set; }
            public Vector2f Size { get; set; }
        }
        //
        public GameConfig Game;
        public MenuConfig Menu;
        public List<BlockConfig> Blocks;

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
            Game = new GameConfig();
            var nodeGame = xmlDocElem.SelectSingleNode("/config/game");
            Game.AssetsFolder = nodeGame.Attributes["assetsFolder"].Value;
            Game.FontName = GetAssetFullName(nodeGame.Attributes["fontName"].Value);
            if (!File.Exists(Game.FontName))
                throw new FileNotFoundException("Ficheiro da fonte não encontrado!", Game.FontName);
            // - Background
            var nodeGameBackground = nodeGame.SelectSingleNode("background");
            Game.Background = new GameConfig.BackgroundConfig();
            Game.Background.TextureName = GetAssetFullName(nodeGameBackground.Attributes["textureName"].Value);
            if (!File.Exists(Game.Background.TextureName))
                throw new FileNotFoundException("Ficheiro de textura não encontrado!", Game.Background.TextureName);
            Game.Background.GameCoords = new Vector2f(
                Convert.ToSingle(nodeGameBackground.Attributes["gameCoordX"].Value),
                Convert.ToSingle(nodeGameBackground.Attributes["gameCoordY"].Value));
            Game.Background.NextBlockCoords = new Vector2f(
                Convert.ToSingle(nodeGameBackground.Attributes["nextBlockCoordX"].Value),
                Convert.ToSingle(nodeGameBackground.Attributes["nextBlockCoordY"].Value));

            // Config/Menu
            Menu = new MenuConfig();
            var nodeMenu = xmlDocElem.SelectSingleNode("/config/menu");
            // - Background
            var nodeMenuBackground = nodeMenu.SelectSingleNode("background");
            Menu.Background = new MenuConfig.BackgroundConfig();
            Menu.Background.TextureName = GetAssetFullName(nodeMenuBackground.Attributes["textureName"].Value);
            if (!File.Exists(Menu.Background.TextureName))
                throw new FileNotFoundException("Ficheiro de textura não encontrado!", Menu.Background.TextureName);

            // Blocks
            var nodeBlocks = xmlDocElem.SelectNodes("/config/blocks/block");
            Blocks = new List<BlockConfig>();
            foreach (XmlNode nodeBlock in nodeBlocks)
            {
                var block = new BlockConfig();
                block.TextureName = nodeBlock.Attributes["textureName"].Value;
                string[] blockLines = nodeBlock.InnerText.Trim().Split('\n');
                block.Size = new Vector2f(blockLines[0].Trim().Length, blockLines.Length);
                block.Parts = new List<Vector2i>();
                for (int l = 0; l < block.Size.Y; l++)
                {
                    for (int c = 0; c < block.Size.X; c++)
                    {
                        if (blockLines[l].Trim().Substring(c, 1) == "1")
                            block.Parts.Add(new Vector2i(c, l));
                    }
                }
                Blocks.Add(block);
            }
        }

        //
        public string GetAssetFullName(string name)
        {
            return $"{Game.AssetsFolder}\\{name}";
        }
    }
}
