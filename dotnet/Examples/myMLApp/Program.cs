// See https://aka.ms/new-console-template for more information

using MyMLApp;

var sampleData = new SentimentModel.ModelInput()
{
    Col0 = "this restaurant was awesome!"
};

var result = SentimentModel.Predict(sampleData);

// If Prediction is 1, sentiment is "Positive"; otherwise, sentiment is "Negative"
var sentiment = result.PredictedLabel == 1 ? "Positive" : "Negative";
Console.WriteLine($"Text: {sampleData.Col0}\nSentiment: {sentiment}");