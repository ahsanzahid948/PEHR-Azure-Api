using Application.DTOs;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Support.Practice.Commands
{
   public class CreateClientCommentCommand:IRequest<Response<int>>
    {
        public PayLoad payLoad;
        public string clientComment;
        public long entitySeqNum;
        public CreateClientCommentCommand(PayLoad payload,long entitySeq,string comment)
        {
            payLoad = payload;
            entitySeqNum = entitySeq;
            clientComment = comment;
        }
    }
}
