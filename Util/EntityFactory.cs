using System.Collections.Generic;
using DSProject.Model;

namespace DSProject.Util
{
    //Classe para centralizar a criação das listas de entidades dos integrantes
    public class EntityFactory
    {
        #region 

        /// <summary>
        /// Cria uma lista com os dispositivos a partir da string separada por ';'
        /// </summary>
        public List<Device> CreateDevices(string devices)
        {
            List<Device> _lstDevices = new List<Device>();

            if (string.IsNullOrEmpty(devices))
                return _lstDevices;

            string[] devicesArray = Utils.StringIntoArray(devices);

            if (devicesArray != null && devicesArray.Length > 0)
            {
                foreach (string device in devicesArray)
                {
                    _lstDevices.Add(new Device
                    {
                        Description = device
                    });
                }
            }

            return _lstDevices;
        }

        /// <summary>
        /// Cria a lista de habilidades de cada participante
        /// </summary>
        public List<Ability> CreateAbilities(string abilities)
        {
            List<Ability> _lstAbilities = new List<Ability>();

            if (string.IsNullOrEmpty(abilities))
                return _lstAbilities;

            string[] abilitiesArray = null;

            abilitiesArray = Utils.StringIntoArray(abilities);

            if (abilitiesArray != null && abilitiesArray.Length > 0)
            {
                foreach (string ability in abilitiesArray)
                {
                    _lstAbilities.Add(new Ability
                    {
                        Description = ability
                    });
                }
            }

            return _lstAbilities;
        }

        /// <summary>
        /// Cria a lista com os esportes que o integrante pratica
        /// </summary>
        public List<Sport> CreateSports(string sports)
        {
            List<Sport> _lstSports = new List<Sport>();

            if (string.IsNullOrEmpty(sports))
                return _lstSports;

            string[] _sportsArray = Utils.StringIntoArray(sports);

            if (_sportsArray != null && _sportsArray.Length > 0)
            {
                foreach (string sport in _sportsArray)
                {
                    _lstSports.Add(new Sport
                    {
                        Description = sport
                    });
                }
            }

            return _lstSports;
        }

        /// <summary>
        /// Cria a lista de instrumentos (As que o integrante possui e as que ele toca)
        /// </summary>
        public List<Instrument> CreateInstruments(string instruments)
        {
            List<Instrument> _lstInstruments = new List<Instrument>();

            if (string.IsNullOrEmpty(instruments))
                return _lstInstruments;

            string[] _instrumentArray = Utils.StringIntoArray(instruments);

            if (_instrumentArray != null && _instrumentArray.Length > 0)
            {
                foreach (string instrument in _instrumentArray)
                {
                    _lstInstruments.Add(new Instrument
                    {
                        Description = instrument
                    });
                }
            }

            return _lstInstruments;
        }

        /// <summary>
        /// Cria a lista de instrumentos (As que o integrante possui e as que ele toca)
        /// </summary>
        public List<InstrumentPlayed> CreateInstrumentsPlayed(string instruments)
        {
            List<InstrumentPlayed> _lstInstrumentsPlayed = new List<InstrumentPlayed>();

            if (string.IsNullOrEmpty(instruments))
                return _lstInstrumentsPlayed;

            string[] _instrumentArray = Utils.StringIntoArray(instruments);

            if (_instrumentArray != null && _instrumentArray.Length > 0)
            {
                foreach (string instrument in _instrumentArray)
                {
                    _lstInstrumentsPlayed.Add(new InstrumentPlayed
                    {
                        Description = instrument
                    });
                }
            }

            return _lstInstrumentsPlayed;
        }

        /// <summary>
        /// Cria a lista de idiomas do participante
        /// </summary>
        public List<Language> CreateLanguages(string languages)
        {
            List<Language> _lstLanguages = new List<Language>();

            if (string.IsNullOrEmpty(languages))
                return _lstLanguages;

            string[] _languageArray = Utils.StringIntoArray(languages);

            if (_languageArray != null && _languageArray.Length > 0)
            {
                foreach (string language in _languageArray)
                {
                    _lstLanguages.Add(new Language
                    {
                        Description = language
                    });
                }
            }

            return _lstLanguages;
        }

        /// <summary>
        /// Cria a lista de áreas de conhecimento dos integrantes
        /// </summary>
        public List<Knowledge> CreateKnowledges(string knowledges)
        {
            List<Knowledge> _lstKnowledges = new List<Knowledge>();

            if (string.IsNullOrEmpty(knowledges))
                return _lstKnowledges;

            string[] _knowledgeArray = Utils.StringIntoArray(knowledges);

            if (_knowledgeArray != null && _knowledgeArray.Length > 0)
            {
                foreach (string knowledge in _knowledgeArray)
                {
                    _lstKnowledges.Add(new Knowledge
                    {
                        Description = knowledge
                    });
                }
            }

            return _lstKnowledges;
        }

        #endregion
    }

}