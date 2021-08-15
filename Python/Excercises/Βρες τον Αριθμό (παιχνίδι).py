import random
x = "n"
synolika = 0
print()
print("Έναρξη")
while x != "τέλος":
    print("Νέος γύρος")
    counter = 0
    num = random.randint(1,100)
    while True:
        x = input("Δώσε αριθμό (1-100) ή 'τέλος' για να σταματήσεις: ").strip()
        if x.isnumeric() and int(x) == num:
            points = 10-counter
            if points <0:  points = 0
            synolika = synolika + points
            print("Το Βρήκες! Κέρδισες ",points, " πόντους. \nΑποτυχημένες προσπάθειες:",counter,"\nΣύνολο πόντων: ",synolika)
            print()
            print()
            break
        elif x.isnumeric() and int(x)>0 and int(x)<=100:
            if int(x) > num: print("Είναι μικρότερος.")
            if int(x) < num: print("Είναι μεγαλύτερος.")
            counter = counter +1
        elif x=="τέλος": break
print("Κέρδισες συνολικά ",synolika," πόντους!")
