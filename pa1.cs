//This program is written by Adalia Spadafora
//This program will allow two users to play the boardgame isolation
using System;
using static System.Console;

namespace pa1
{
    class Program
    {
		//initialize static variable
		static bool[ , ] board; 
		static bool turnA = true; 
		static string name1;
		static string name2;
		static string[] letters = { "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
		static string space1;
		static string space2; 
		static string start1;
		static string start2; 
		static string read;
		static bool validBoard = false;
		static int row;
		static int col;
			
		static void display(){
			//board display variables
            const string h  = "\u2500"; // horizontal line
            const string v  = "\u2502"; // vertical line
            const string tl = "\u250c"; // top left corner
            const string tr = "\u2510"; // top right corner
            const string bl = "\u2514"; // bottom left corner
            const string br = "\u2518"; // bottom right corner
            const string vr = "\u251c"; // vertical join from right
            const string vl = "\u2524"; // vertical join from left
            const string hb = "\u252c"; // horizontal join from below
            const string ha = "\u2534"; // horizontal join from above
            const string hv = "\u253c"; // horizontal vertical cross
            const string sp = " ";      // space
            const string pa = "A";      // pawn A
            const string pb = "B";      // pawn B
            const string bb = "\u25a0"; // block
            const string fb = "\u2588"; // full block
            const string lh = "\u258c"; // left half block
            const string rh = "\u2590"; // right half block
            
            Console.Clear();
                
            // Draw the top board boundary
            Write( "    " );
            for(int i = 0; i < board.GetLength(1); i ++){
				Write(" {0}  ", letters[i]);
			}
			WriteLine();
			Write( "   " );
            for(int c = 0; c < board.GetLength(1); c ++){
				
                if(c == 0){
					 Write(tl);
				}
				
                Write( "{0}{0}{0}", h );
                
                if( c == board.GetLength( 1 ) - 1 ){ 
					Write( "{0}", tr );
				}else{
					Write( "{0}", hb );
				}
            }
            
            WriteLine();
            
            // Draw the board rows.
            for(int r = 0; r < board.GetLength( 0 ); r ++){
                Write(" {0} ", letters[r]);
                // Draw the row contents.
                for(int c = 0; c < board.GetLength( 1 ); c ++){
                    if( c == 0 ){
						Write( v );
					}
					//draw the platforms/pawns/blanks
					if( r == Array.IndexOf( letters, space1.Substring(0,1)) && c == Array.IndexOf( letters, space1.Substring(1,1))){
						Write( "{1}{0}{1}{2}", pa, sp, v);
					}else if( r == Array.IndexOf( letters, space2.Substring(0,1)) && c == Array.IndexOf( letters, space2.Substring(1,1))){
						Write( "{1}{0}{1}{2}", pb, sp, v);
					}else if((r == Array.IndexOf( letters, start1.Substring(0,1)) && c == Array.IndexOf( letters, start1.Substring(1,1))) || (r == Array.IndexOf( letters, start2.Substring(0,1)) && c == Array.IndexOf( letters, start2.Substring(1,1)))){
						Write("{0}{1}{0}{2}", sp, bb, v); 
					}else if(board[r, c]){
						Write( "{0}{1}{2}{3}", rh, fb, lh, v);
					}else{
						Write( "{0}{1}", "   ", v );
					}
                }
                WriteLine( );
                // Draw the boundary after the row.
                if( r != board.GetLength( 0 ) - 1 ){ 
                    Write( "   " );
                    for( int c = 0; c < board.GetLength( 1 ); c ++ ){
                        if( c == 0 ){
							 Write( vr );
						}
						
                        Write( "{0}{0}{0}", h );
                        if( c == board.GetLength( 1 ) - 1 ){
							 Write( "{0}", vl ); 
                        }else{
							Write( "{0}", hv );
						}
                    }
                    WriteLine( );
                }else{
                    Write( "   " );
                    for( int c = 0; c < board.GetLength( 1 ); c ++ ){
                        if( c == 0 ){
							Write( bl );
						}
                        Write( "{0}{0}{0}", h );
                        if( c == board.GetLength( 1 ) - 1 ){
							Write( "{0}", br ); 	
                        }else{
							Write( "{0}", ha );
						}
					}
					WriteLine( );
				}
			}
		}
		
		
		static void init(){
			//ask the users their names, the board size, and the starting position
			WriteLine("What is the name of player one?");
			read = ReadLine();
			if (read.Length == 0){
				name1 = "Player One";
			
			}else{
				name1 = read;
			}
			
			WriteLine("What is the name of player two?");
			read = ReadLine();
			if (read.Length == 0){
				name2 = "Player Two";
			}else{
				name2 = read;
			}
			
			while(!validBoard){
			
				WriteLine("How many rows?");
				read = ReadLine();
				if (read.Length == 0){
					row = 6; 
					validBoard = true;
				}else{
					row = int.Parse(read);
					if (row > 3 && row < 27){
						validBoard = true;
					}
				}
			}
			
			validBoard = false;
			
			while(!validBoard){
				WriteLine("How many columns?");
				read = ReadLine();
				if (read.Length == 0){
					col = 8;
					validBoard = true;
				}else{
					col = int.Parse(read);
					if (col > 3 && col < 27){
						validBoard = true;
					}
				}
			}
			
			board = new bool[row, col];
			
			WriteLine(name1 + " what is your starting position (row, column)? ");
			read = ReadLine();
			if (read.Length == 0){
				space1 = "ca";
				start1 = space1;
			}else{
				space1 = read;
				start1 = space1;
			}
			
			WriteLine(name2 + " what is your starting position (row, column)? ");
			read = ReadLine();
			if (read.Length == 0){
				space2 = "dh";
				start2 = space2;
			}else{
				space2 = read;
				start2 = space2;
			}
			
			//set the array for the board
			for (int i = 0; i < row; i++){
				for ( int j = 0; j < col; j++){
					board[i,j] = true; 
				}
			}
		}
		
		static void makeMove(){
		// method to make the move
			bool validMove = false; 
			
			while(!validMove){
				
				if(turnA){
					WriteLine(name1 + " ");
				}else{
					WriteLine(name2 + " ");
				}
				
				Write("Enter a valid move: ");
				string move = ReadLine();
				//check to see if the move is valid, if it is, then make the move
				
				if(move.Length != 4){
					
					WriteLine("your move should be 4 letters!");
					
				}else{
					int moverow = Array.IndexOf(letters, move.Substring(0,1));
					int movecol = Array.IndexOf(letters, move.Substring(1,1));
					int removerow = Array.IndexOf(letters, move.Substring(2,1));
					int removecol = Array.IndexOf(letters, move.Substring(3,1));
								
								
/*								
player cannot move:	
* stay in the same spot               moverow == space1 row && move col == space 1 col   space1 
* move to where the other player is    moverow == space2 row && move col == space 2 col 
* move to an empty space
* move to anywhere thats not beside 
* 
* 
* player cannot remove: 
* where they are 
* starting positions 
* where other player is 
* 	*/			
					if (turnA){
						if((moverow == Array.IndexOf(letters, space1.Substring(0,1))) && (movecol == Array.IndexOf(letters, space1.Substring(1,1)))){
							 WriteLine("Cannot move there");
						}else if(moverow == Array.IndexOf(letters, space2.Substring(0,1)) && (movecol == Array.IndexOf(letters, space2.Substring(1,1)))){
							WriteLine("Cannot move there");
						}else if((removerow == Array.IndexOf(letters, space1.Substring(0,1)))&&(removecol == Array.IndexOf(letters, space1.Substring(1,1)))){
							 WriteLine("Cannot remove that space");
						}else if((removerow == Array.IndexOf(letters, space2.Substring(0,1))) && (removecol == Array.IndexOf(letters, space2.Substring(1,1)))){
							 WriteLine("Cannot remove that space");
						}else if((removerow == Array.IndexOf(letters, start1.Substring(0,1))) && (removecol == Array.IndexOf(letters, start1.Substring(1,1)))){
							WriteLine("Cannot remove that space");
						}else if((removerow == Array.IndexOf(letters, start2.Substring(0,1)))&&(removecol == Array.IndexOf(letters, start2.Substring(1,1)))){
							 WriteLine("Cannot remove that space");
						}else{
							if(board[moverow, movecol]){						
								if(((Array.IndexOf(letters, space1.Substring(1,1)) - movecol) == 1) || (Array.IndexOf(letters, space1.Substring(1,1)) - movecol == -1) || (Array.IndexOf(letters, space1.Substring(1,1)) - movecol == 0)){								
									if(((Array.IndexOf(letters, space1.Substring(0,1)) - moverow) == 1) || ((Array.IndexOf(letters, space1.Substring(0,1)) - moverow) == -1) || ((Array.IndexOf(letters, space1.Substring(0,1)) - moverow) == 0)){
										if(board[removerow, removecol]){
											board[removerow, removecol] = false;
											space1 = move.Substring(0,2);
											validMove = true;
										}
									}else{
										WriteLine("Invalid Move");
									}
								}else{
									WriteLine("Invalid Move");
								}	
							}
						}
					}else{
						if((moverow == Array.IndexOf(letters, space1.Substring(0,1))) && (movecol == Array.IndexOf(letters, space1.Substring(1,1)))){
							 WriteLine("Cannot move there");
						}else if(moverow == Array.IndexOf(letters, space2.Substring(0,1)) && (movecol == Array.IndexOf(letters, space2.Substring(1,1)))){
							WriteLine("Cannot move there");
						}else if((removerow == Array.IndexOf(letters, space1.Substring(0,1)))&&(removecol == Array.IndexOf(letters, space1.Substring(1,1)))){
							 WriteLine("Cannot remove that space");
						}else if((removerow == Array.IndexOf(letters, space2.Substring(0,1))) && (removecol == Array.IndexOf(letters, space2.Substring(1,1)))){
							 WriteLine("Cannot remove that space");
						}else if((removerow == Array.IndexOf(letters, start1.Substring(0,1))) && (removecol == Array.IndexOf(letters, start1.Substring(1,1)))){
							WriteLine("Cannot remove that space");
						}else if((removerow == Array.IndexOf(letters, start2.Substring(0,1)))&&(removecol == Array.IndexOf(letters, start2.Substring(1,1)))){
							 WriteLine("Cannot remove that space");
						}else{
							if(board[moverow, movecol]){
								if(((Array.IndexOf(letters, space2.Substring(1,1)) - movecol) == 1) || ((Array.IndexOf(letters, space2.Substring(1,1)) - movecol) == -1) || ((Array.IndexOf(letters, space2.Substring(1,1)) - movecol) == 0)){
									if(((Array.IndexOf(letters, space2.Substring(0,1)) - moverow) == 1) || ((Array.IndexOf(letters, space2.Substring(0,1)) - moverow) == -1) || ((Array.IndexOf(letters, space2.Substring(0,1)) - moverow) == 0)){
										if(board[removerow, removecol]){
											board[removerow, removecol] = false;
											space2 = move.Substring(0,2);
											validMove = true;
										}
									}else{
										WriteLine("Invalid Move");
									}
									WriteLine("Invalid Move");
								}	
							}
						}
					}
				}	
			}
		}
			
        static void Main(string[] args){
			//run the game
			init();
			while(true){
				WriteLine(turnA);
				display();
				makeMove();
				if (turnA == true){
					turnA = false;
				}else{
					turnA = true;
				}
			}
        }
    }
}
