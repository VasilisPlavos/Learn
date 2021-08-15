#diavazei ena arxio alla den to krataei
txt = open("maze.txt","r")
m = txt.readlines()
x = []
E = 0   #einai to telos (END)
E0 = 0  #an giro apo to E iparxoun 4 midenika i 3 midenika kai 1 S h telos lavirinthou, midenika

for x1 in m:
    for x2 in x1:
        x.append(x2)
        x3 = len(x)     #x3 tha einai sinolo ta psifia
        n = len(x1)

#psaxnei na vrei tin afeteria ( S = start ) kai vazei 2aria giro tis
for i in range (x3):
    if x[i] == '\n':    #afto edo kanei ta \n (alages gramis) midenika kathos einai san tixos
        x[i] = 0
    if x[i] == '0':     #afto kanei tous tixous tou lavirinthou apo apo '0' se 0
        x[i] = 0
    if x[i]=='S':
        S = i
        if x[i-1]=='1':
            x[i-1]=2            
        if x[i+1]=='1':
            x[i+1]=2
        if x3 > i + n:
            if x[i+n]=='1':
                x[i+n]=2
        if i - n > 0:
            if x[i-n]=='1':
                x[i-n]=2

#edo vazo stin lista to simio sto opoio vriskete i arxi (px 1:1 (S))
sol = str(int(S/n)+1)+':'+str(S - int(S/n)*n +1)+' (S)'
SOL = [(sol)]


num = 1     # einai to epomeno noumero pou tha skanarei, to vazo ena giati me to
            # pou tha mpei sto while kanei +1 kai to kanei dio
while num < x3:
    num = num + 1
    for i in range (x3):
        if x[i] == 'E':     #afto vriskei to simio pou vriskete o termatismos
            E = i           #kai to krataei
        if x[i] == num:
            if x[i-1]=='1':
                x[i-1]=(num + 1)
            if x[i+1]=='1':
                x[i+1]=(num + 1)
            if x3 > i + n:
                if x[i+n]=='1':
                    x[i+n]=(num + 1)
            if i - n > 0:
                if x[i-n]=='1':
                    x[i-n]=(num + 1)

#edo vazo stin lista to simio sto opoio vriskete to telos (px 1:1 (S), 4:5 (G))
sol = str(int(E/n)+1)+':'+str(E - int(E/n)*n +1)+' (G)'
SOL.append(sol)

#edo kanei oti den mu xriazete midenika kai tin afeteria tin kanei 1
for i in range (x3):
    if x[i] == 'S':
        x[i] = 1
    if x[i] == '1':
        x[i] = 0



#edo vrisko poios einai o megaliteros arithmos giro apo to E
num = 0
p = E
if E+1 < x3 and x[E+1] > num:
        num = x[E+1]
        p = E+1             #to p krataei to simio opou einai o protos megaliteros arithmos dipla apo to telos
        sol = str(int((E+1)/n)+1)+':'+str((E+1) - int((E+1)/n)*n +1)
        move = ['Left']
if E+n < x3 and x[E+n] > num:
        num = x[E+n]
        p = E+n
        sol = str(int((E+n)/n)+1)+':'+str((E+n) - int((E+n)/n)*n +1)
        move = ['Up']
if E-1 > 0 and x[E-1] > num:
        num = x[E-1]
        p = E-1
        sol = str(int((E-1)/n)+1)+':'+str((E-1) - int((E-1)/n)*n +1)
        move = ['Right']
if E-n > 0 and x[E-n] > num:
        num = x[E-n]
        p = E-n
        sol = str(int((E-n)/n)+1)+':'+str((E-n) - int((E-n)/n)*n +1)
        move = ['Down']
if x[p] == 'E':
    E0 = 4  #ean to E0 ginei tesera simenei oti o megaliteros arithmos
            #giro apo to telos ine o eaftos tu, ara i ine kolita stin afeteria i den iparxi lisi


SOL.insert(1, sol)
moves = num
num = num - 1
while num > 0:
    if p+1 < x3 and x[p+1] == num:
        p = p+1
        sol = str(int((p)/n)+1)+':'+str((p) - int((p)/n)*n +1)
        SOL.insert(1, sol)
        move.insert(0, "Left")
        num = num - 1
    elif p+n < x3 and x[p+n] == num:
        p = p+n
        sol = str(int((p)/n)+1)+':'+str((p) - int((p)/n)*n +1)
        SOL.insert(1, sol)
        move.insert(0, "Up")
        num = num - 1
    elif p-1 > 0 and x[p-1] == num:
        p = p - 1
        sol = str(int((p)/n)+1)+':'+str((p) - int((p)/n)*n +1)
        SOL.insert(1, sol)
        move.insert(0, "Right")
        num = num - 1
    elif p-n < x3 and x[p-n] == num:
        p = p-n
        sol = str(int((p)/n)+1)+':'+str((p) - int((p)/n)*n +1)
        SOL.insert(1, sol)
        move.insert(0, "Down")
        num = num - 1

if E0 == 4:
    print ('No way out! We pray for you.')
else:
    del SOL[1]
    print ('The solution is ', SOL)
    print ('To escape follow the instructions: ',move)
    
            
    
    





