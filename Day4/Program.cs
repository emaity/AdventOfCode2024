using System.IO;

bool findXmasHelper(int r, int c, int pos, Direction dir, char[,] wordSearch, int rowLen, int colLen) {
    if(r < 0 || r>=rowLen || c<0 || c>=colLen) {
        return false;
    }
    if(pos > 3)
        return true;

    // Check current letter
    char curr = wordSearch[r,c];
    switch(pos) {
        case 0:
            if(curr != 'X')
                return false;
            break;
        case 1:
            if(curr != 'M')
                return false;
            break;
        case 2:
            if(curr != 'A')
                return false;
            break;
        case 3:
            if(curr != 'S')
                return false;
            break;
    }

    // Continue search
    switch(dir) {
        case Direction._N:
            return findXmasHelper(r-1, c, pos+1, Direction._N, wordSearch, rowLen, colLen);
        case Direction._NE:
            return findXmasHelper(r-1, c+1, pos+1, Direction._NE, wordSearch, rowLen, colLen);
        case Direction._E:
            return findXmasHelper(r, c+1, pos+1, Direction._E, wordSearch, rowLen, colLen);
        case Direction._SE:
            return findXmasHelper(r+1, c+1, pos+1, Direction._SE, wordSearch, rowLen, colLen);
        case Direction._S:
            return findXmasHelper(r+1, c, pos+1, Direction._S, wordSearch, rowLen, colLen);
        case Direction._SW:
            return findXmasHelper(r+1, c-1, pos+1, Direction._SW, wordSearch, rowLen, colLen);
        case Direction._W:
            return findXmasHelper(r, c-1, pos+1, Direction._E, wordSearch, rowLen, colLen);
        case Direction._NW:
            return findXmasHelper(r-1, c-1, pos+1, Direction._NW, wordSearch, rowLen, colLen);
        default:
            // Should not reach here?
            return false;
    }
}

int findXmas(int r, int c, char[,] wordSearch, int rowLen, int colLen) {
    int count = 0;

    if(r>0) {
        // NW
        if(c>0 && wordSearch[r-1,c-1] == 'M' && findXmasHelper(r-1, c-1, 1, Direction._NW, wordSearch, rowLen, colLen))
            count++;
        
        // N
        if(wordSearch[r-1,c] == 'M' && findXmasHelper(r-1, c, 1, Direction._N, wordSearch, rowLen, colLen))
            count++;
        
        // NE
        if(c<colLen-1 && wordSearch[r-1,c+1] == 'M' && findXmasHelper(r-1, c+1, 1, Direction._NE, wordSearch, rowLen, colLen))
            count++;
    }

    // E
    if(c<colLen-1 && wordSearch[r,c+1] == 'M' && findXmasHelper(r, c+1, 1, Direction._E, wordSearch, rowLen, colLen))
        count++;
    
    if(r<rowLen-1) {
        // SE
        if(c<colLen-1 && wordSearch[r+1,c+1] == 'M' && findXmasHelper(r+1, c+1, 1, Direction._SE, wordSearch, rowLen, colLen))
            count++;

        // S
        if(wordSearch[r+1,c] == 'M' && findXmasHelper(r+1, c, 1, Direction._S, wordSearch, rowLen, colLen))
            count++;
        
        // SW
        if(c>0 && wordSearch[r+1,c-1] == 'M' && findXmasHelper(r+1, c-1, 1, Direction._SW, wordSearch, rowLen, colLen))
            count++;
    }

    // W
    if(c>0 && wordSearch[r,c-1] == 'M' && findXmasHelper(r, c-1, 1, Direction._W, wordSearch, rowLen, colLen))
        count++;
    
    return count;
}

char[,] s= {{'X','M','A','S'}, {'M','W','S', 'A'}, {'A','R','R','M'}, {'S','Q','Q','X'}};
int rowLen = 4;
int colLen = 4;

int count = 0;
for (int r=0 ; r<rowLen ; r++) {
    for (int c=0 ; c<colLen ; c++) {
        if(s[r,c] == 'X')
            count += findXmas(r, c, s, rowLen, colLen);
    }
}

Console.WriteLine(count);

enum Direction {
    _N, _NE, _E, _SE, _S, _SW, _W, _NW
}