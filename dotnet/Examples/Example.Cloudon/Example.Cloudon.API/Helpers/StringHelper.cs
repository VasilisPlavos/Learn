using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Example.Cloudon.API.Helpers
{
    public class StringHelper
    {
        public static string ToSha256String(string value)
        {
            using var hash = SHA256.Create();
            return string.Concat(hash
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                .Select(x => x.ToString("x2"))
            );
        }


        public static string ProductsContentAsString()
        {
            return
                "{\n    \"success\": true,\n    \"data\": [\n        {\n            \"externalId\": \"328530\",\n            \"code\": \"K00142505\",\n            \"description\": \"ONKO BCG 100 ΚΟΝΙΣ ΚΑΙ ΔΙΑΛΥΤΗΣ ΓΙΑ ΕΝΔΟΚΥΣΤΙΚΟ ΕΝΑΙΩΡΗΜΑ BT x 1 AMP + 1 AMP SOLV\",\n            \"name\": \"ONKO\",\n            \"barcode\": \"2809191501017\",\n            \"retailPrice\": \"179.29\",\n            \"wholesalePrice\": \"145.81\",\n            \"discount\": \"0\"\n        },\n        {\n            \"externalId\": \"328529\",\n            \"code\": \"K00142504\",\n            \"description\": \"ABIRATERONE ACCORD F.C.TAB 500MG/TAB 60 Χ 1 ΔΙΣΚΙΑ ΜΙΑΣ ΔΟΣΗΣ ΣΕ BLISTERS PVC/PVDC/ALU\",\n            \"name\": \"ABIRATERONE ACCORD\",\n            \"barcode\": \"2803290402022\",\n            \"retailPrice\": \"0\",\n            \"wholesalePrice\": \"0\",\n            \"discount\": \"0\"\n        },\n        {\n            \"externalId\": \"328528\",\n            \"code\": \"K00142503\",\n            \"description\": \"XAREBEN® F.C.TAB 20MG/TAB BT X 30 TABS ΣΕ PVC/PVDC/ALUMINIUM BLISTERS\",\n            \"name\": \"XAREBEN®\",\n            \"barcode\": \"2803288503014\",\n            \"retailPrice\": \"0\",\n            \"wholesalePrice\": \"0\",\n            \"discount\": \"0\"\n        },\n        {\n            \"externalId\": \"328527\",\n            \"code\": \"K00142502\",\n            \"description\": \"XAREBEN® F.C.TAB 15MG/TAB BT X 30 TABS ΣΕ PVC/PVDC/ALUMINIUM BLISTERS\",\n            \"name\": \"XAREBEN®\",\n            \"barcode\": \"2803288502017\",\n            \"retailPrice\": \"0\",\n            \"wholesalePrice\": \"0\",\n            \"discount\": \"0\"\n        },\n        {\n            \"externalId\": \"328526\",\n            \"code\": \"K00142501\",\n            \"description\": \"XAREBEN® F.C.TAB 10MG/TAB BT X 30 TABS ΣΕ PVC/PVDC/ALUMINIUM BLISTERS\",\n            \"name\": \"XAREBEN®\",\n            \"barcode\": \"2803288501027\",\n            \"retailPrice\": \"0\",\n            \"wholesalePrice\": \"0\",\n            \"discount\": \"0\"\n        },\n        {\n            \"externalId\": \"328525\",\n            \"code\": \"K00142500\",\n            \"description\": \"XAREBEN® F.C.TAB 10MG/TAB BT X 10 TABS ΣΕ PVC/PVDC/ALUMINIUM BLISTERS\",\n            \"name\": \"XAREBEN®\",\n            \"barcode\": \"2803288501010\",\n            \"retailPrice\": \"0\",\n            \"wholesalePrice\": \"0\",\n            \"discount\": \"0\"\n        },\n        {\n            \"externalId\": \"328524\",\n            \"code\": \"K00142499\",\n            \"description\": \"ABIRATERONE MYLAN F.C.TAB 500MG/TAB 60 ΔΙΣΚΙΑ ΣΕ BLISTERS (ALU-PVC/PE/PVDC)\",\n            \"name\": \"ABIRATERONE MYLAN\",\n            \"barcode\": \"2803286901065\",\n            \"retailPrice\": \"0\",\n            \"wholesalePrice\": \"0\",\n            \"discount\": \"0\"\n        },\n        {\n            \"externalId\": \"328523\",\n            \"code\": \"K00142498\",\n            \"description\": \"FULVESTRANT/DEMO INJ.SO.PFS 250MG/5ML 1 BT x 2 PF.SYR. (TYPE I glass)x5 ml+2 βελόνες ασφαλείας\",\n            \"name\": \"FULVESTRANT/DEMO\",\n            \"barcode\": \"2803286001024\",\n            \"retailPrice\": \"0\",\n            \"wholesalePrice\": \"0\",\n            \"discount\": \"0\"\n        },\n        {\n            \"externalId\": \"328522\",\n            \"code\": \"K00142497\",\n            \"description\": \"FINGOLIMOD MYLAN CAPS 0.5MG/CAP 28 ΚΑΨΑΚΙΑ - ΣΕ BLISTERS PVC/PE/PVDC/ALU\",\n            \"name\": \"FINGOLIMOD MYLAN\",\n            \"barcode\": \"2803285601188\",\n            \"retailPrice\": \"0\",\n            \"wholesalePrice\": \"0\",\n            \"discount\": \"0\"\n        },\n        {\n            \"externalId\": \"328521\",\n            \"code\": \"K00142496\",\n            \"description\": \"PITAVASTATIN/SANDOZ F.C.TAB 4MG/TAB BT X 28 TABS (BLISTER PVC/PVDC/ALU)\",\n            \"name\": \"PITAVASTATIN/SANDOZ\",\n            \"barcode\": \"2803274503028\",\n            \"retailPrice\": \"0\",\n            \"wholesalePrice\": \"0\",\n            \"discount\": \"0\"\n        }\n    ]\n}";
        }
    }
}