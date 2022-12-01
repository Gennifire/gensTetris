using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tetris.BlockFiles.tilePieces;
using Tetris.GameManager;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        //holds tiles images for block
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Resources/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/TileRed.png", UriKind.Relative))
        };

        //holds images for each block type
        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Resources/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Resources/Block-Z.png", UriKind.Relative))
        };

        //1 image control for each cell in the game grid
        private readonly Image[,] imageControls;

        //values to speed up block movement as layer increases score
        private readonly int maxDelay = 1000;
        private readonly int minDelay = 75;
        private readonly int delayDecrease = 25;

        private GameState gameState = new GameState();

        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.gameScreen);
        }

        //set up game canvas to draw game block from tile images
        private Image[,] SetupGameCanvas(GameScreen grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;

            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    //for each position
                    Image imageControl = new Image
                    {
                        //create a new image control 25 pixels both width and height
                        Width = cellSize,
                        Height = cellSize
                    };

                    // - 2 to push the first to rows above the canvas
                    Canvas.SetTop(imageControl, (r - 2) * cellSize + 10);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }

            return imageControls;
        }

        private void DrawGrid(GameScreen grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach (BlockPosition p in block.BlockPositions())
            {
                imageControls[p.Row, p.Column].Opacity = 1;
                imageControls[p.Row, p.Column].Source = tileImages[block.id];
            }
        }

        private void DrawNextBlock(BlockQueue blockQueue)
        {
            Block next = blockQueue.nextBlock;
            NextImage.Source = blockImages[next.id];
        }

        private void DrawHeldBlock(Block heldBlock)
        {
            if (heldBlock == null)
            {
                HoldImage.Source = blockImages[0];
            }
            else
            {
                HoldImage.Source = blockImages[heldBlock.id];
            }
        }

        private void DrawGhostBlock(Block block)
        {
            int dropDistance = gameState.BlockDropDistance();

            foreach (BlockPosition p in block.BlockPositions())
            {
                //ghost block is shown directly under current block 
                //adding row and current block
                imageControls[p.Row + dropDistance, p.Column].Opacity = 0.25;
                imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.id];
            }
        }

        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.gameScreen);
            DrawGhostBlock(gameState.CurrentBlock);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.blockQueue);
            DrawHeldBlock(gameState.heldBlock);
            ScoreText.Text = $"Score: {gameState.score}";
        }

        private async Task GameLoop()
        {
            Draw(gameState);

            
            while (!gameState.gameOver)
            {
                int delay = Math.Max(minDelay, maxDelay - (gameState.score * delayDecrease));
                await Task.Delay(delay);
                gameState.MoveBlockDown();
                Draw(gameState);
            }

            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.score}";
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.gameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft();
                    break;
                case Key.Right:
                    gameState.MoveBlockRight();
                    break;
                case Key.Down:
                    gameState.MoveBlockDown();
                    break;
                case Key.Up:
                    gameState.RotateBlockClockWise();
                    break;
                case Key.Z:
                    gameState.RotateBlockCounterClockWise();
                    break;
                case Key.C:
                    gameState.HoldBlock();
                    break;
                case Key.Space:
                    gameState.DropBlock();
                    break;
                default:
                    return;
            }

            Draw(gameState);
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState(); //start new game state
            GameOverMenu.Visibility = Visibility.Hidden; //hide game over menu
            await GameLoop(); //restart game
        }
    }
}
