﻿using Prism.Events;
using ProductivityHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityHub.Application.Events.TaskEvents
{
    public class TaskDeletedEvent: PubSubEvent<Guid>
    {
    }
}
