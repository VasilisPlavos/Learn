# Άσκηση 16_1_template
# Κρίση εργασίας: (βαθμολόγηση όπως παρακάτω)
import random
import time

marker = {'Παίκτης 1': 'X', 'Παίκτης 2': 'O', }

def display_board(board): # 4 μονάδες
    print("+"+6*"---"+"+")
    print("|7    |8    |9    |")
    print("|  {}  |  {}  |  {}  |".format(board[7],board[8],board[9]))
    print("|     |     |     |")
    print("+"+6*"---"+"+")
    print("|4    |5    |6    |")
    print("|  {}  |  {}  |  {}  |".format(board[4],board[5],board[6]))
    print("|     |     |     |")
    print("+"+6*"---"+"+")
    print("|1    |2    |3    |")
    print("|  {}  |  {}  |  {}  |".format(board[1],board[2],board[3]))
    print("|     |     |     |")
    print("+"+6*"---"+"+")
    #εμφάνισε την κατάσταση της τρίλιζας


def choose_first():
    x = random.randint(1,2)
    if x == 1: return "Παίκτης 1"
    if x == 2: return "Παίκτης 2"
    # 2 μονάδες
    #κλήρωση για το ποιος θα παίξει πρώτος
    # επιστρέφει είτε 'Παίκτης 1' είτε 'Παίκτης 2'


def display_score(score): # 2 μονάδες
    print("ΤΕΛΙΚΟ ΣΚΟΡ")
    print("Παίκτης 1 : {}".format(score.get("Παίκτης 1",0)))
    print("Παίκτης 2 : {}".format(score.get("Παίκτης 2",0)))
    #Τυπώνει το τελικό σκορ


def place_marker(board, marker, position): # 2 μονάδες
    board[position] = marker
    #Τοποθετεί στη θέση position του board τον marker


def win_check(board,mark): # 4 μονάδες *
    if board[1] == mark and board[2] == mark and board[3] == mark:
        return True
    elif board[4] == mark and board[5] == mark and board[6] == mark:
        return True
    elif board[7] == mark and board[8] == mark and board[9] == mark:
        return True
    elif board[1] == mark and board[4] == mark and board[7] == mark:
        return True
    elif board[2] == mark and board[5] == mark and board[8] == mark:
        return True
    elif board[3] == mark and board[6] == mark and board[9] == mark:
        return True
    elif board[1] == mark and board[5] == mark and board[9] == mark:
        return True
    elif board[3] == mark and board[5] == mark and board[7] == mark:
        return True
    else:
        return False
    #επιστρέφει True αν το σύμβολο mark έχει σχηματίσει τρίλιζα

def board_check(board): # 2 μονάδες
    a = 0
    for i in board:
        if i == ' ':
            a = a+1
    if a==1:
        return True
    else:
        return False
    #επιστρέφει True αν υπάρχουν ακόμη κενά τετράγωνα

 
def player_choice(board, turn): # 2 μονάδες *
    # Ο Παίκτης turn επιλέγει τετράγωνο
    x = 100
    while True:
        while x!='1' and x!='2' and x!='3' and x!='4' and x!='5' and x!='6' and x!='7' and x!='8' and x!='9':
            x = input("{}  [ {} ]: Διάλεξε τετράγωνο: (1-9): ".format(turn,marker[turn]))
        x = int(x)
        if board[x]==' ':
            break
    return x
    # Επιστρέφει έναν ακέραιο στο διάστημα [1,9]


def replay(): # 1 μονάδα
    x = 0
    while x != "ναι" and x!="οχι":
        x = input("Θέλεις να ξαναπαιξεις (ναι/οχι): ")
    if x == "ναι":
        return True
    else:
        return False
    # Ρωτάει τον χρήστη αν θέλει να ξαναπαίξει και επιστρέφει True αν ναι.


def next_player(turn): # 1 μονάδα
    if turn == "Παίκτης 1":
        return "Παίκτης 2"
    else:
        return "Παίκτης 1"
    #επιστρέφει τον επόμενο παίκτη που πρέπει να παίξει


def main():
    score = {} # λεξικό με το σκορ των παικτών
    print('Αρχίζουμε!\nΓίνεται κλήρωση ', end = '')
    for t in range(10):
        print(".", flush='True', end=' ')
        time.sleep(0.2)
    print()
    # η μεταβλητή turn αναφέρεται στον παίκτη που παίζει
    turn = choose_first() 
    print("\nΟ " + turn + ' παίζει πρώτος.')
    # η μεταβλητή first αναφέρεται στον παίκτη που έπαιξε πρώτος
    first = turn 
    game_round = 1 # γύρος παιχνιδιού
    while True:
        # Καινούργιο παιχνίδι
        # Δημιουργία λίστας 10 στοιχείων βλέπε μάθημα 2 σελ.7 σημειώσεων
        theBoard = [' '] * 10
        # Αφήστε το πρώτο στοιχείο δηλαδή το theBoard[0] κενό έτσι ώστε 
        # το index να αντιστοιχεί στην ονοματοδότηση των τετραγώνων 
        game_on = True  #ξεκινάει το παιχνίδι
        while game_on:
            display_board(theBoard) #Εμφάνισε την τρίλιζα
            # ο παίκτης turn επιλέγει θέση
            position = player_choice(theBoard, turn)
            # τοποθετείται η επιλογή του
            place_marker(theBoard, marker[turn], position) 
            if win_check(theBoard, marker[turn]): # έλεγχος αν νίκησε
                display_board(theBoard)
                print('Νίκησε ο '+ turn)
                score[turn] = score.get(turn, 0) + 1
                game_on = False
            # έλεγχος αν γέμισε το ταμπλό χωρίς νικητή
            elif board_check(theBoard): 
                display_board(theBoard)
                print('Ισοπαλία!')
                game_on = False
            else: # αλλιώς συνεχίζουμε με την κίνηση του επόμενου παίκτη
                turn = next_player(turn)
        if not replay():
            ending = ''
            if game_round>1 : ending = 'υς'
            print("Μετά {} γύρο{}".format(game_round, ending))
            display_score(score) # έξοδος ... τελικό σκορ
            break
        else :
            game_round += 1
            # στο επόμενο παιχνίδι ξεκινάει ο άλλος παίκτης
            turn = next_player(first) 
            first = turn
main()
