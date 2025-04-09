from sklearn.linear_model import LinearRegression
from random import randint

# This script can predict if number is mobile, local or wrong (under 9 digits)
# only for Greek numbers

RANDINTSTART = 2100000000
RANDITEND = 3100000000
TRAIN_SET_COUNT = 100

TRAIN_INPUT = list()
TRAIN_OUTPUT = list()

for i in range(100000):
  a = randint(0, 999999999)
  TRAIN_INPUT.append([a])
  TRAIN_OUTPUT.append(10)

for i in range(100000):
  a = randint(2100000000, 2110000000)
  TRAIN_INPUT.append([a])
  TRAIN_OUTPUT.append(50)

for i in range(100000):
  a = randint(6900000000, 6970000000)
  TRAIN_INPUT.append([a])
  TRAIN_OUTPUT.append(90)


predictor = LinearRegression(n_jobs=-1)
predictor.fit(X=TRAIN_INPUT, y=TRAIN_OUTPUT)

# enter the number that you want to test here
for i in range(10):
  number = input('Enter phone number here: ')
  number = int(number)
  X_TEST = [[number]]
  outcome = predictor.predict(X=X_TEST)
  print(outcome)
  print(predictor.coef_)