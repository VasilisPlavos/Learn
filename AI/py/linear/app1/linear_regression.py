from random import randint
from sklearn.linear_model import LinearRegression

# Here we start creating a dataset to train our model
# Our model has this parameters x (a,b,c)
# Our model has the goal y: a + (2*b) + (3*c) = goal
TRAIN_SET_LIMIT = 1000
TRAIN_SET_COUNT = 100

TRAIN_INPUT = list()
TRAIN_OUTPUT = list()
for i in range(TRAIN_SET_COUNT):
    a = randint(0, TRAIN_SET_LIMIT)
    b = randint(0, TRAIN_SET_LIMIT)
    c = randint(0, TRAIN_SET_LIMIT)
    op = a + (2*b) + (3*c)
    TRAIN_INPUT.append([a, b, c])
    TRAIN_OUTPUT.append(op)
# Here we finished with the dataset creation


# We create our model here. Predictor is the name of the model
predictor = LinearRegression(n_jobs=-1)
predictor.fit(X=TRAIN_INPUT, y=TRAIN_OUTPUT)

# X_TEST is our input. The predictor is compering this input with the model in order to spit the y
X_TEST = [[10, 20, 30]]
outcome = predictor.predict(X=X_TEST)
coefficients = predictor.coef_

print('Outcome : {}\nCoenfficients: {}'.format(outcome, coefficients))

