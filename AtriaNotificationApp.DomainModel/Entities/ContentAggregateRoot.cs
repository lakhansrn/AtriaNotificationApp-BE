using AtriaNotificationApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Entities
{
    public class ContentAggregateRoot : IAggregateRoot
    {
        public ContentAggregateRoot(Content content)
        {
            Content = content;
        }

        public Content Content
        {
            get;
            private set;
        }
    }
}
