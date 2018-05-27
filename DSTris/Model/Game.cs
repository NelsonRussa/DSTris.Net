using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SFML.Window.Keyboard;

namespace DSTris.Model
{
    class Game: IRender
    {
        // 
        public enum GameState
        {
            Initializing,
            Menu,
            Playing,
            Paused,
            GameOver,
            ExitGame
        }
        public GameState State { get; set; } = GameState.Menu;
        public string FontName { get { return Config.Game.FontName; }  }
        //
        private Sprite BackgroundMenu { get; set; }
        private Sprite BackgroundGame { get; set; }
        private Text txtGameOver;
        private Text txtPaused;
        private GameBlock CurrentBlock { get; set; } = null;
        private GameBlock NextBlock { get; set; } = null;
        private GameBlockPart[,] PlayArea = null;
        private Config Config;
        private Random Rnd;
        private float DropVelocity = 1f;
        private Clock GameTime;
        public Statistics Statistics;
        private enum PositionValidation
        {
            OK,
            OutOfHorizontalBound,
            OutOfVerticalBound,
            ColisionWithAnotherBlock,
        }

        // Retornar os objectos do jogo que podem ser desenhados
        // pela ordem de prioridade
        public IEnumerable<Drawable> DrawObjects
        {
            get
            {
                // Menu
                if (State == GameState.Menu)
                {
                    yield return BackgroundMenu;
                }
                else 
                {
                    // Fundo do jogo
                    yield return BackgroundGame;
                
                    // Área do jogo
                    for (int l = 0; l < Config.Game.PlayableSize.Y; l++)
                    {
                        for (int c = 0; c < Config.Game.PlayableSize.X; c++)
                        {
                            if (PlayArea[l, c] != null)
                                yield return PlayArea[l, c].Sprite;
                        }
                    }
                    //
                    // Bloco em jogo
                    if (CurrentBlock.Visible)
                        yield return CurrentBlock;

                    // Proximo bloco
                    if (NextBlock.Visible)
                        yield return NextBlock;

                    // Desenhar as estatisticas dos 
                    // blocos que já sairam
                    yield return Statistics;

                    // Texto foreground
                    if (State == GameState.GameOver)
                        yield return txtGameOver;
                    else if (State == GameState.Paused)
                        yield return txtPaused;
                }
            }
        }

        //
        public Game()
        {
        }

        // Inicializar o jogo
        public void Initialize()
        {
            Config = new Config();
            try
            {
                Config.Load();
            }
            catch (FileNotFoundException ex)
            {
                throw new ConfigFileMissingException($"Ficheiro de configuração não encontrado", ex.FileName);
            }
            Statistics = new Statistics(Config.Game.Background.StatsCoords, Config.Game.Background.StatsMaxY);

            //
            BackgroundMenu = new Sprite(new Texture(Config.Menu.Background.TextureName));
            BackgroundGame = new Sprite(new Texture(Config.Game.Background.TextureName));
            //
            txtGameOver = new Text("Game Over", new Font(FontName));
            txtGameOver.CharacterSize = 50;
            txtGameOver.Style = Text.Styles.Bold;
            txtGameOver.Color = new Color(Color.Red);
            txtGameOver.Position = new Vector2f(400, 300);
            //
            txtPaused = new Text("P A U S E", new Font(FontName));
            txtPaused.CharacterSize = 50;
            txtPaused.Style = Text.Styles.Bold;
            txtPaused.Color = new Color(Color.White);
            txtPaused.Position = new Vector2f(425, 300);
            //
            Rnd = new Random();
            GameTime = new Clock();

            ReiniciarNivel();
        }

        //
        public void KeyPressed(Keyboard.Key keyCode)
        {
            //
            if (State == GameState.Menu)
            {
                switch (keyCode)
                {
                    case Keyboard.Key.Escape:
                        State = GameState.ExitGame;
                        break;

                    case Keyboard.Key.Return:
                        ReiniciarNivel();
                        State = GameState.Playing;
                        break;
                }
            }
            else if (State == GameState.Playing)
            {
                switch (keyCode)
                {
                    case Key.Escape:
                        GameOver();
                        break;

                    case Key.Space:
                        State = GameState.Paused;
                        break;

                    case Key.Left:
                        MoveCurrentBlock(-1, 0);
                        break;

                    case Key.Right:
                        MoveCurrentBlock(1, 0);
                        break;

                    case Key.Down:
                        MoveCurrentBlock(0, 1);
                        break;

                    case Key.Up:
                        RotateCurrentBlock();
                        break;
                }
            }
            else if (State == GameState.Paused)
            {
                switch (keyCode)
                {
                    case Key.Escape:
                        GameOver();
                        break;

                    case Key.Space:
                        State = GameState.Playing;
                        break;
                }
            }
            else if (State == GameState.GameOver)
            {
                switch (keyCode)
                {
                    case Key.Escape:
                    case Key.Return:
                    case Key.Space:
                        State = GameState.Menu;
                        break;
                }
            }
        }

        //
        public void Close()
        {
            State = GameState.ExitGame;
        }

        //
        public void Update()
        {
            //
            if (State == GameState.Playing)
            {
                if (GameTime.ElapsedTime.AsSeconds() > DropVelocity)
                {
                    GameTime.Restart();
                    MoveCurrentBlock(0, 1);
                }
            }
        }

        // Cada vez que começa um nivel novo
        private void ReiniciarNivel()
        {
            //
            PlayArea = new GameBlockPart[Config.Game.PlayableSize.Y, Config.Game.PlayableSize.X];
            // 
            NextBlock=null;
            NewCurrentBlock();
        }

        //
        private void GameOver()
        {
            State = GameState.GameOver;
        }

        // Gerar um novo bloco aleatório que será o proximo em jogo
        // Posicionar nas coordenadas definidas na configuração
        private void NewNextBlock()
        {
            NextBlock = GetRandomBlock();
            NextBlock.ScreenPosition = Config.Game.Background.NextBlockCoords;
        }

        // Gerar novo bloco em jogo. Será o que estava definido como proximo bloco em jogo
        private void NewCurrentBlock()
        {
            // Se ainda não tinha definido o proximo bloco (acontece quando inicia o nivel)
            if (NextBlock == null)
                NewNextBlock();

            // O bloco em jogo é o que estava definido como próximo bloco
            CurrentBlock = NextBlock;
            // Posicionar o bloco em jogo no topo/meio da área de jogo
            SetBlockGridPosition(CurrentBlock, new Vector2i((Config.Game.PlayableSize.X - CurrentBlock.Size.X) / 2, 0));

            // Guardar o ID do bloco, para mostrar as estatisticas
            Statistics.Add(CurrentBlock.ConfigBlockID, CurrentBlock.StatsColor);

            // Definir o próximo bloco
            NewNextBlock();
        }

        // Gerar novo bloco aleatório
        private GameBlock GetRandomBlock()
        {
            //
            var configBlock = Config.Blocks.Skip(Rnd.Next(Config.Blocks.Count)).First();
            var texture = new Texture($"{Config.Game.AssetsFolder}\\{configBlock.TextureName}");

            //
            return new GameBlock(configBlock.ID, configBlock.StatsColor, texture, configBlock.Parts);
        }

        // Verifica se uma determinada posição é valida para um bloco
        // (se não está fora da área de jogo e se nao tem blocos lá posicionados)
        private PositionValidation ValidatePosition(List<GameBlockPart> gameBlockParts, Vector2i gridPosition)
        {
            //
            foreach (var part in gameBlockParts)
            {
                // Verifica se está dentro da área de jogo
                if (gridPosition.X + part.Position.X < 0 || gridPosition.X + part.Position.X >= Config.Game.PlayableSize.X)
                    return PositionValidation.OutOfHorizontalBound;
                if (gridPosition.Y + part.Position.Y < 0 || gridPosition.Y + part.Position.Y >= Config.Game.PlayableSize.Y)
                    return PositionValidation.OutOfVerticalBound;

                // Verifica se não tem blocos lá posicionados
                if (PlayArea[gridPosition.Y + part.Position.Y, gridPosition.X + part.Position.X] != null)
                    return PositionValidation.ColisionWithAnotherBlock;
            }

            //
            return PositionValidation.OK;
        }

        // Mover o bloco em jogo na direcao definida (caso seja valida)
        private void MoveCurrentBlock(int horizontalStep, int verticalStep)
        {
            var currentGridPosition = CurrentBlock.GridPosition;
            currentGridPosition.X += horizontalStep;
            currentGridPosition.Y += verticalStep;

            // Validar posição para onde se pretende mover o bloco
            var validatePosition = ValidatePosition(CurrentBlock.Parts, currentGridPosition);

            // Caso seja uma posição válida
            if (validatePosition == PositionValidation.OK)
                SetBlockGridPosition(CurrentBlock, currentGridPosition);
            else if (validatePosition == PositionValidation.OutOfVerticalBound || (verticalStep != 0 && validatePosition == PositionValidation.ColisionWithAnotherBlock))
            {
                // Se a posição onde se quer posicionar o bloco está ocupada, quer dizer que o bloco pousou
                // Então tem de se guardar esse bloco na área de jogo e gerar novo bloco
                StoreCurrentBlock();
                NewCurrentBlock();

                // Se não foi possível colocar o novo bloco, é porque perdeu o jogo
                if (ValidatePosition(CurrentBlock.Parts, CurrentBlock.GridPosition) != PositionValidation.OK)
                {
                    CurrentBlock.Visible = false;
                    GameOver();
                }
            }
        }

        // Rodar bloco em jogo
        private void RotateCurrentBlock()
        {
            List<GameBlockPart> newBlockParts = new List<GameBlockPart>();
            foreach (var part in CurrentBlock.Parts)
            {
                // Transpor
                var newPosition = new Vector2i(part.Position.Y, part.Position.X);
                // Inverter
                newPosition = new Vector2i(CurrentBlock.Size.Y - part.Position.Y, part.Position.X);

                //
                newBlockParts.Add(new GameBlockPart(part.Texture, newPosition));
            }

            // Se a posição do bloco rodado está livre
            if (ValidatePosition(newBlockParts, CurrentBlock.GridPosition) == PositionValidation.OK)
            {
                // Atualiza bloco em jogo com a nova rotação
                CurrentBlock.SetNewParts(newBlockParts);
                foreach (var part in CurrentBlock.Parts)
                    part.Move(CurrentBlock.ScreenPosition);
            }
        }

        // Guarda o bloco em jogo na área de jogo
        // (usado quando o bloco pousa)
        private void StoreCurrentBlock()
        {
            foreach (var part in CurrentBlock.Parts)
            {
                var newBlockPart = new GameBlockPart(part.Texture, new Vector2i(0, 0));
                var screenPosition = ConvertGridToScreen(new Vector2i(CurrentBlock.GridPosition.X + part.Position.X, CurrentBlock.GridPosition.Y + part.Position.Y));
                newBlockPart.Move(screenPosition);
                PlayArea[CurrentBlock.GridPosition.Y + part.Position.Y, CurrentBlock.GridPosition.X + part.Position.X] = newBlockPart;
            }

            // Após o bloco pousar, verifica se criou linhas completas
            CheckLines(CurrentBlock.GridPosition.Y, CurrentBlock.GridPosition.Y + CurrentBlock.Size.Y - 1);
        }
        
        // Verificar se as linhas entre os limites definidos estão completas
        // (não vale a pena verificar a área toda, visto apenas onde a peça pousou é que podem ter linhas completas
        private void CheckLines(int gridY1, int gridY2)
        {
            List<int> completedLines = new List<int>();
            bool completed;

            //
            for (int line = gridY1; line <= gridY2; line++)
            {
                completed = true;
                for (int col = 0; col < Config.Game.PlayableSize.X; col++)
                {
                    if (PlayArea[line, col] == null)
                    {
                        completed = false;
                        break;
                    }
                }
                if (completed)
                    completedLines.Add(line);
            }

            // Se completou algumas linhas, remove (e move tudo que está acima, de for a fazer cair)
            if (completedLines.Any())
                RemoveLines(completedLines);

        }

        // Remover as linhas definidas (quer dizer que ficaram completas após pousar uma peça)
        private void RemoveLines(List<int> lines)
        {
            //
            foreach (int line in lines)
            {
                for (int c = 0; c < Config.Game.PlayableSize.X; c++)
                    PlayArea[line, c] = null;

                // 
                for (int l = line; l >= 0; l--)
                {
                    for (int c = 0; c < Config.Game.PlayableSize.X; c++)
                    {
                        if (l > 0)
                            PlayArea[l, c] = PlayArea[l - 1, c];
                        else
                            PlayArea[l, c] = null;

                        if (PlayArea[l, c] != null)
                        {
                            var newScreenPosition = ConvertGridToScreen(new Vector2i(c, l));
                            PlayArea[l, c].Move(newScreenPosition);
                        }

                    }
                }
            }
        }

        // Retornar as coordenadas no ecra, com base na grelha de jogo
        private Vector2f ConvertGridToScreen(Vector2i gridPosition)
        {
            Vector2f screenPosition = Config.Game.Background.GameCoords;

            screenPosition.X += gridPosition.X * Config.Game.GridSize.X;
            screenPosition.Y += gridPosition.Y * Config.Game.GridSize.Y;

            return screenPosition;
        }

        // Atribuir as coordenadas a um bloco com base na grelha de jogo
        private void SetBlockGridPosition(GameBlock block, Vector2i gridPosition)
        {
            block.GridPosition = gridPosition;
            block.ScreenPosition = ConvertGridToScreen(gridPosition);
        }
    }
}