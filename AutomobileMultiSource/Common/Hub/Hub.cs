using AutomobileMultiSource.Common.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomobileMultiSource.Common.Hub
{
    public class Hub
    {

        private HttpServerUtilityBase Server;

        public Hub(HttpServerUtilityBase server)
        {
            this.Server = server;
        }

        public string Get()
        {

            /* Je pense que ici, l'idée est de concaténer les différentes sources de données dans un certain format
             * Il faut donc coder une sorte de Concatenator avec plusieurs fonctions pour les différentes sources de données.
             * 
             * On peut lui donner soit toutes les données en texte, soit toutes les données en XML etc. et il nous sort un texte
             * dans le format choisi (égale au format des données) où tout est concaténé. 
             * 
             * A réfléchir...
             */

            TextDatasource textData = new TextDatasource(this.Server);

            return textData.ToJson();

            throw new NotImplementedException();
        }

    }
}