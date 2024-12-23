using System.Text;

Console.OutputEncoding = Encoding.UTF8;
int DemanderHauteur()
{
    int hauteur;
    do
    {
        Console.Write("Entrez la hauteur du sapin (entre 1 et 30) (/!\\mettre la console en pleine ecran !): ");
    } while (!int.TryParse(Console.ReadLine(), out hauteur) || hauteur < 1 || hauteur > 30);
    return hauteur;
}

void DessinerFeuilles(int hauteur)
{
    int largeurMax;
    if (hauteur % 2 == 0)
    {
        largeurMax = 2 * hauteur + 1;
    }
    else
    {
        largeurMax = 2 * hauteur;
    }

    Random randomBoule = new Random();
    ConsoleColor[] couleurs = { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Magenta };

    int consoleWidth = Console.WindowWidth;

    for (int i = 0; i < hauteur; i++)
    {
        int nbEtoiles = 2 * i + 1;
        int nbEspaces = (largeurMax - nbEtoiles) / 2;
        int position = (consoleWidth - largeurMax) / 2 + nbEspaces;

        if (position >= 0)
        {
            Console.SetCursorPosition(position, i + 1);
            for (int j = 0; j < nbEtoiles; j++)
            {
                if(nbEtoiles == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("★");
                    Console.ResetColor();
                }
                else if (randomBoule.Next(15) < 1)
                {
                    Console.ForegroundColor = couleurs[randomBoule.Next(couleurs.Length)];
                    Console.Write('●');
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('▲');
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }
    }
}

void DessinerTronc(int hauteur)
{
    int largeurTronc = 3;
    int hauteurTronc = hauteur / 3;
    int consoleWidth = Console.WindowWidth;
    int position = (consoleWidth - largeurTronc) / 2;

    for (int i = 0; i < hauteurTronc; i++)
    {
        if (position >= 0)
        {
            Console.SetCursorPosition(position, hauteur + i + 1);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("|||");
            Console.ResetColor();
        }
    }
}

void GenererNeige(int consoleWidth, int consoleHeight, int hauteur)
{
    Random random = new Random();
    int nombreFlocons = consoleWidth * consoleHeight / 20;

    // eviter que les flocons touche le tronc
    int largeurTronc = 3;
    int hauteurTronc = hauteur / 3;
    int positionTronc = (consoleWidth - largeurTronc) / 2;


    for (int i = 0; i < nombreFlocons; i++)
    {
        int x, y;
        do
        {
            x = random.Next(consoleWidth);
            y = random.Next(consoleHeight);
        } while ((x >= positionTronc && x < positionTronc + largeurTronc && y >= hauteur && y < hauteur + hauteurTronc));

        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("¤");
    }
}

static void PlayNote(int frequency, int duration)
{
    Console.Beep(frequency, duration);
}
void Musique()
{
    while(true)
    {
        // Notes frequencies in Hz
        int E = 659;     // Mi
        int F = 698;     // Fa
        int G = 784;     // Sol
        int C = 523;     // Do
        int D = 587;     // Ré

        // Durations in milliseconds
        int Quarter = 400;  // Noire
        int Half = Quarter * 2;  // Blanche
        int Eighth = Quarter / 2; // Croche
        int Whole = Quarter * 4; // 400ms * 4 = 1600ms

        PlayNote(E, Quarter); PlayNote(E, Quarter); PlayNote(E, Half);
        PlayNote(E, Quarter); PlayNote(E, Quarter); PlayNote(E, Half);
        PlayNote(E, Quarter); PlayNote(G, Quarter); PlayNote(C, Quarter);
        PlayNote(D, Quarter); PlayNote(E, Whole);

        PlayNote(F, Quarter); PlayNote(F, Quarter); PlayNote(F, Quarter);
        PlayNote(F, Quarter); PlayNote(F, Quarter); PlayNote(E, Quarter);
        PlayNote(E, Quarter); PlayNote(E, Eighth); PlayNote(E, Eighth);
        PlayNote(E, Quarter); PlayNote(D, Quarter); PlayNote(D, Quarter);
        PlayNote(E, Quarter); PlayNote(D, Half); PlayNote(G, Half);
    }
}

Task.Factory.StartNew(() => Musique());

int hauteur = DemanderHauteur();
while (true)
{
    Console.Clear();
    DessinerFeuilles(hauteur);
    DessinerTronc(hauteur);
    GenererNeige(Console.WindowWidth, Console.WindowHeight, hauteur);
    Thread.Sleep(400);
}