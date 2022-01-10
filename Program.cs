/*
*   Per la generazione di una cartella ho scelto di generare un array bidimensionale,
*   ma non è una condizione indispensabile: si possono tranquillamente usare array
*   monodimensionali, diventa solo più macchinoso.
*   Su ogni riga della matrice verranno inseriti i dieci numeri di ogni decina, poi
*   verranno rimescolati e da ogni riga verranno estratti i primi tre numeri per 
*   comporre nelle rispettive colonne una cartella con ventisette numeri.
*   Da tale cartella verranno poi tolti casualmente quattro numeri su ogni riga per
*   arrivare alla versione finale della cartella da visualizzare che conterrà i
*   quindici numeri necessari, con le singole decine organizzate per colonne e ordinate
*   in ordine crescente.
*   Il programma contiene alcune parti che non abbiamo affrontato insieme in classe,
*   ma le ho inserite di proposito per dare, a chi lo vorrà, qualche occasione di 
*   approfondimento. Per chi invece preferisce andare piano, le varie parti le affronteremo
*   insieme a tempo debito.
*
*   Poi sicuramente si può fare meglio: chi lo migliora?!
*/

// Creazione matrice primaria
// La prima colonna della cartella contiene di numeri 1 -> 9, mentre l'ultima
// contiene i numeri 80 -> 90 con uno sbilanciamento delle varie decine che, nella
// prima e nell'ultima colonna decine reali non sono. Da qui l'inserimento di zeri
// che poi verranno scartati in fase di generazione della cartella.

int numero = 0;
int [,] MatricePrimaria = new int [9,11];
int [,] Cartella = new int[3,9];

// Popolamento matrice primaria
for (int i = 0; i < 9; i++)
{
    for (int j = 0; j < 10; j++)
    {
        MatricePrimaria[i,j] = numero++;
    }
}
MatricePrimaria[8,10] = 90;

//Rimescolamento Matrice Primaria riga per riga
RimescolaMatricePerRiga(MatricePrimaria);

// Creazione cartella grezza
// Scansione delle singole righe della matrice primaria
for (int riga = 0; riga < 9; riga++)
{
    int colonnaMatrice = 0;
    int rigaCartella = 0;
    int colonnaCartella = riga;

    // Analisi colonne matrice primaria
    do
    {
        while(MatricePrimaria[riga, colonnaMatrice] == 0)
        {
            colonnaMatrice++;
        }
        Cartella[rigaCartella, colonnaCartella] = MatricePrimaria[riga, colonnaMatrice];
        rigaCartella++;
        colonnaMatrice++;
        if (rigaCartella == 3)
        {
            break;
        }
    } while (rigaCartella < 3);   
}

OrdinaColonne(Cartella);

NormalizzaCartella(Cartella);

StampaCartella(Cartella);

void NormalizzaCartella(int [,] Matrice)
{
    // Toglie a caso tre numeri per riga
    Random rnd = new Random();
    int zeri = 0;
    int tmp = 0;

    for (int riga = 0; riga < Matrice.GetLength(0); riga++)
    {
        while(zeri < 4)
        {
            tmp = rnd.Next(0,9);
            if (Matrice[riga, tmp] != 0)
            {
                Matrice[riga, tmp] = 0;
                zeri++;
            }    
        }
        zeri = 0;
    }
    
    return;
}

void OrdinaColonne(int[,] Matrice)
{   
    // Ordina in ordine crescente le colonne di una matrice bidimensionale
    // con algoritmo Bubble Sort non ottimizzato
    /*
        scambio ← true
        while scambio do
            scambio ← false
            for i ← 0 to length(A)-1  do
                if A[i] > A[i+1] then
                    swap( A[i], A[i+1] )
                    scambio ← true
    */
    for (int colonna = 0; colonna < Matrice.GetLength(1); colonna++)
    {
        bool scambio = true;
        int tmp = 0;
        while (scambio)
        {
            scambio = false;
            for (int riga = 0; riga < Matrice.GetLength(0)-1; riga++)
            {
                if ((riga+1 < Matrice.GetLength(1)) && Matrice[riga, colonna] > Matrice[riga+1, colonna])
                {
                    tmp = Matrice[riga, colonna];
                    Matrice[riga, colonna] = Matrice[riga+1, colonna];
                    Matrice[riga+1, colonna] = tmp;
                    scambio = true;
                }
            }
        }  
    }
    return;
}

//Usata per debug - Scommentare se serve
// void StampaMatrice(int [,] Matrice)
// {
//     int righe = Matrice.GetLength(0);
//     int colonne = Matrice.GetLength(1);

//     for (int i = 0; i < righe; i++)
//     {
//         for (int j = 0; j < colonne; j++)
//         {
//             Console.Write(Matrice[i,j] + " ");
//         }
//         Console.WriteLine();
//     }
// }

void StampaCartella(int[,] Matrice)
{   
    int righe = Matrice.GetLength(0);
    int colonne = Matrice.GetLength(1);
    
    Console.WriteLine("+--------------------------------------------+");
    Console.WriteLine("+           ITT'S TOMBOLA TIME!!!            +");
    Console.WriteLine("+--------------------------------------------+");

    for (int i = 0; i < righe; i++)
    {
        Console.Write("|");
        for (int j = 0; j < colonne; j++)
        {
            if (Matrice[i, j] != 0)
                Console.Write(" {0, 2} |", Matrice[i, j]);
            else
                Console.Write("    |");
        }
        Console.WriteLine();
        Console.WriteLine("+--------------------------------------------+");
    }
}

void RimescolaMatricePerRiga (int[,] Matrice)
{
    Random rnd = new Random();
    int tmp = 0;
    int righe = Matrice.GetLength(0);

    for (int riga = 0; riga < righe; riga++)
    {
        for (int i = 0; i < 1000; i++)
        {
            int indexA = rnd.Next(0,6);
            int indexB = rnd.Next(6,11);
            tmp = Matrice[riga, indexA];
            Matrice[riga, indexA] = Matrice[riga, indexB];
            Matrice[riga, indexB] = tmp;
        }
    } 

    return;
}