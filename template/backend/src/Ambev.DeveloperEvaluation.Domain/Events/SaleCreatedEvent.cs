﻿using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public record SaleCreatedEvent(Guid SaleId) : INotification;
