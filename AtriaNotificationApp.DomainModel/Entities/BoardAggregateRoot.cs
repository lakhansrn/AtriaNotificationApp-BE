using AtriaNotificationApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtriaNotificationApp.DAL.Entities
{
    public class BoardAggregateRoot : IAggregateRoot
    {

        public BoardAggregateRoot(Board board)
        {
            if(board == null)
            {
                throw new InvalidOperationException("Board cannot be null ofr the root");
            }
            this.Board = board;
        }

        public Board Board
        {
            get;
            private set;
        }
    }
}
