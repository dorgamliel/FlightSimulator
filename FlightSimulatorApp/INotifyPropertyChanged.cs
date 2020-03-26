using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{        //ניצור דלגייט שיגדיר איך פונקציה שדואגת לשלוח הודעה על שינוי תכונה צריכה להראות.
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
    interface INotifyPropertyChanged
    {
        //ניצור איבנט שכל מי שיממש את הממשק יכיל אותו, והוא יודיע לכל האוברזרברים שרשומים אליו שמשהו השתנה.
        event PropertyChangedEventHandler propertyChanged;
    }
}
