using Microsoft.Azure.Mobile.Server;

namespace app20193Service.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
        public string Name_text { get; set; }


    }
}