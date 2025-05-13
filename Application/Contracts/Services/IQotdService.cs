using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.Qotd;

namespace Application.Contracts.Services;

public interface IQotdService
{
    Task<QuoteOfTheDayViewModel> GetQuoteOfTheDayAsync();
}