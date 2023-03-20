using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader.Utils
{
    class LocalizationModloader
    {
        private readonly IStringLocalizer<LocalizationModloader> _localizer = null!;

        public LocalizationModloader(IStringLocalizer<LocalizationModloader> localizer) =>
            _localizer = localizer;

        [return: NotNullIfNotNull(nameof(_localizer))]
        public string? GetGreetingMessage()
        {
            LocalizedString localizedString = _localizer["GreetingMessage"];
            return localizedString;
        }
    }


    public class ParameterizedLocalizationService
    {
        private readonly IStringLocalizer _localizer = null!;

        public ParameterizedLocalizationService(IStringLocalizerFactory factory) =>
            _localizer = factory.Create(typeof(ParameterizedLocalizationService));

        [return: NotNullIfNotNull(nameof(_localizer))]
        public string? GetFormattedMessage(DateTime dateTime, double dinnerPrice)
        {
            LocalizedString localizedString = _localizer["DinnerPriceFormat", dateTime, dinnerPrice];
            return localizedString;
        }
    }
}
