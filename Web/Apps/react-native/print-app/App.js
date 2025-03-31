import { useState } from "react";
import { ScrollView, View, TouchableOpacity, Text, Linking, Alert } from "react-native";
import tailwind from "tailwind-rn";

export default function App() {
  const [isPrinting, setIsPrinting] = useState(false);
  const [clientSidePrinting, setClientSidePrinting] = useState(false);
  const [serverSidePrinting, setServerSidePrinting] = useState(false);

  const print_client_side = () => {
    setClientSidePrinting(true);
    try {
      var page1 = document.querySelector("#page-1");
      var page2 = document.querySelector("#page-2");
      var wnd = window.open('', '_blank');

      var documentContent = `
      <html>
      <head>
          <title>Print</title>
          <style>
              ${[...document.styleSheets]
          .map(styleSheet => {
            try {
              return [...styleSheet.cssRules].map(rule => rule.cssText).join('\n');
            } catch (e) {
              return '';
            }
          })
          .join('\n')}
          </style>
      </head>
      <body>
          ${page1.outerHTML}
          ${page2.outerHTML}
      </body>
      </html>
  `;

      wnd.document.open();
      wnd.document.write(documentContent);
      wnd.document.close();
      wnd.focus();
      wnd.print();
    } catch (error) {
      Alert.alert("Error", "PDF cannot open! Please try again.");
    }
    finally {
      setClientSidePrinting(false);
    }
  }
  

  const print_server_side = async () => {
    setServerSidePrinting(true);
    try {
      const response = await fetch(
        "https://XXX.azurewebsites.net/api/XXX?XXX=XXX"
      );
      const data = await response.json();
      Linking.openURL(data.sasUrl);
    } catch (error) {
      Alert.alert("Error", "PDF cannot open! Please try again.");
    }
    finally {
      setServerSidePrinting(false);
    }
  }

  return (
    <ScrollView style={{ ...tailwind('w-full p-4 bg-yellow-400') }}>
      <View style={{...tailwind('w-full flex flex-row items-center justify-center')}}>
        <TouchableOpacity
          disabled={clientSidePrinting || serverSidePrinting}
          onPress={print_client_side}
          style={{ ...tailwind('p-4 bg-white mb-4 rounded-xl mx-1') }}
        >
          <Text>{clientSidePrinting ? "Printing..." : "1. Print 2 pages to PDF - client side"}</Text>
        </TouchableOpacity>
        <TouchableOpacity
          disabled={clientSidePrinting || serverSidePrinting}
          onPress={print_server_side}
          style={{ ...tailwind('p-4 bg-white mb-4 rounded-xl mx-1') }}
        >
          <Text>{serverSidePrinting ? "Printing..." : "2. Print 2 pages to PDF - server side"}</Text>
        </TouchableOpacity>
      </View>
      <View id="page-1" style={{ aspectRatio: 3508 / 2480, ...tailwind('w-full bg-white mb-4 flex flex-col items-center justify-center') }}>
        <Text style={{ ...tailwind('absolute top-2 left-2') }}>Some top text // Page 1</Text>
        <Text style={{ fontSize: 34 }}>Page 1</Text>
        <Text style={{ ...tailwind('absolute bottom-2 right-2') }}>Some bottom text // Page 1</Text>
        <View style={{...tailwind('absolute top-2 right-2 w-4 h-4 bg-green-400')}} />
        <View style={{...tailwind('absolute bottom-2 left-2 w-4 h-4 rounded-full bg-red-400')}} />
      </View>
      <View id="page-2" style={{ aspectRatio: 3508 / 2480, ...tailwind('w-full bg-white mb-4 flex flex-col items-center justify-center') }}>
        <Text style={{ ...tailwind('absolute top-2 left-2') }}>Some top text // Page 2</Text>
        <Text style={{ fontSize: 34 }}>Page 2</Text>
        <Text style={{ ...tailwind('absolute bottom-2 right-2') }}>Some bottom text // Page 2</Text>
        <View style={{...tailwind('absolute top-2 right-2 w-4 h-4 rounded-full bg-red-400')}} />
        <View style={{...tailwind('absolute bottom-2 left-2 w-4 h-4 bg-green-400')}} />
      </View>
    </ScrollView>
  )
}