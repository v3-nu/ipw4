using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using DnsServerCore.ApplicationCommon;
using TechnitiumLibrary.Net.Dns;
using TechnitiumLibrary.Net.Dns.ResourceRecords;


namespace Ipw4
{
    public sealed class App : IDnsApplication, IDnsAppRecordRequestHandler
    {
        #region variables

        static readonly char[] separators = new char[] { '.', '-' };

        IDnsServer _dnsServer;


        Dictionary<string, byte> ipw4map = new Dictionary<string, byte>
        {
            {"nil", 0},
            {"cat", 1},
            {"sat", 2},
            {"yen", 3},
            {"vat", 4},
            {"sew", 5},
            {"oak", 6},
            {"gel", 7},
            {"sin", 8},
            {"tab", 9},
            {"bag", 10},
            {"old", 11},
            {"gum", 12},
            {"sob", 13},
            {"pad", 14},
            {"ray", 15},
            {"cog", 16},
            {"sub", 17},
            {"bow", 18},
            {"jaw", 19},
            {"win", 20},
            {"lye", 21},
            {"tip", 22},
            {"yak", 23},
            {"cub", 24},
            {"nip", 25},
            {"cue", 26},
            {"pig", 27},
            {"coy", 28},
            {"peg", 29},
            {"ebb", 30},
            {"oar", 31},
            {"rim", 32},
            {"pin", 33},
            {"ban", 34},
            {"net", 35},
            {"tar", 36},
            {"gem", 37},
            {"end", 38},
            {"mop", 39},
            {"hum", 40},
            {"mat", 41},
            {"oil", 42},
            {"web", 43},
            {"hat", 44},
            {"few", 45},
            {"but", 46},
            {"mug", 47},
            {"ask", 48},
            {"dot", 49},
            {"jab", 50},
            {"pie", 51},
            {"add", 52},
            {"bad", 53},
            {"nox", 54},
            {"ion", 55},
            {"den", 56},
            {"ref", 57},
            {"amp", 58},
            {"orb", 59},
            {"elk", 60},
            {"sad", 61},
            {"pod", 62},
            {"why", 63},
            {"fob", 64},
            {"ply", 65},
            {"rum", 66},
            {"dam", 67},
            {"hen", 68},
            {"nun", 69},
            {"nix", 70},
            {"nod", 71},
            {"uni", 72},
            {"shy", 73},
            {"wad", 74},
            {"wax", 75},
            {"nit", 76},
            {"spy", 77},
            {"had", 78},
            {"ash", 79},
            {"job", 80},
            {"eve", 81},
            {"ate", 82},
            {"egg", 83},
            {"toy", 84},
            {"row", 85},
            {"lab", 86},
            {"cut", 87},
            {"keg", 88},
            {"imp", 89},
            {"tan", 90},
            {"toe", 91},
            {"pet", 92},
            {"now", 93},
            {"air", 94},
            {"owl", 95},
            {"pan", 96},
            {"nag", 97},
            {"van", 98},
            {"tux", 99},
            {"wok", 100},
            {"ink", 101},
            {"act", 102},
            {"joy", 103},
            {"zip", 104},
            {"pug", 105},
            {"pay", 106},
            {"lap", 107},
            {"tie", 108},
            {"ego", 109},
            {"ton", 110},
            {"fun", 111},
            {"let", 112},
            {"fox", 113},
            {"map", 114},
            {"rag", 115},
            {"woe", 116},
            {"fry", 117},
            {"mid", 118},
            {"ale", 119},
            {"dew", 120},
            {"ant", 121},
            {"out", 122},
            {"wig", 123},
            {"use", 124},
            {"tel", 125},
            {"cod", 126},
            {"apt", 127},
            {"irk", 128},
            {"kin", 129},
            {"zoo", 130},
            {"sue", 131},
            {"pun", 132},
            {"ran", 133},
            {"aim", 134},
            {"tub", 135},
            {"hit", 136},
            {"con", 137},
            {"ace", 138},
            {"rip", 139},
            {"sum", 140},
            {"she", 141},
            {"kid", 142},
            {"emu", 143},
            {"hay", 144},
            {"gas", 145},
            {"bug", 146},
            {"cry", 147},
            {"ill", 148},
            {"nap", 149},
            {"med", 150},
            {"sun", 151},
            {"bet", 152},
            {"dog", 153},
            {"cab", 154},
            {"jet", 155},
            {"mom", 156},
            {"opt", 157},
            {"not", 158},
            {"yaw", 159},
            {"ear", 160},
            {"top", 161},
            {"rye", 162},
            {"yes", 163},
            {"cup", 164},
            {"tag", 165},
            {"sit", 166},
            {"can", 167},
            {"who", 168},
            {"age", 169},
            {"bid", 170},
            {"ohm", 171},
            {"axe", 172},
            {"elf", 173},
            {"rad", 174},
            {"fit", 175},
            {"eat", 176},
            {"odd", 177},
            {"dry", 178},
            {"rub", 179},
            {"fog", 180},
            {"lag", 181},
            {"bar", 182},
            {"sea", 183},
            {"met", 184},
            {"sip", 185},
            {"pry", 186},
            {"ram", 187},
            {"sow", 188},
            {"new", 189},
            {"dig", 190},
            {"zap", 191},
            {"fan", 192},
            {"app", 193},
            {"log", 194},
            {"gap", 195},
            {"wit", 196},
            {"zen", 197},
            {"urn", 198},
            {"rib", 199},
            {"ham", 200},
            {"all", 201},
            {"wag", 202},
            {"arm", 203},
            {"kit", 204},
            {"dad", 205},
            {"art", 206},
            {"tap", 207},
            {"mow", 208},
            {"abs", 209},
            {"own", 210},
            {"mad", 211},
            {"ufo", 212},
            {"hot", 213},
            {"cow", 214},
            {"pal", 215},
            {"pit", 216},
            {"rat", 217},
            {"eco", 218},
            {"nay", 219},
            {"son", 220},
            {"car", 221},
            {"inn", 222},
            {"gym", 223},
            {"rap", 224},
            {"dim", 225},
            {"red", 226},
            {"hip", 227},
            {"war", 228},
            {"tug", 229},
            {"vin", 230},
            {"pop", 231},
            {"man", 232},
            {"nut", 233},
            {"vet", 234},
            {"max", 235},
            {"nav", 236},
            {"bun", 237},
            {"cap", 238},
            {"lug", 239},
            {"dye", 240},
            {"wee", 241},
            {"bin", 242},
            {"day", 243},
            {"aid", 244},
            {"rod", 245},
            {"set", 246},
            {"ivy", 247},
            {"put", 248},
            {"lax", 249},
            {"gag", 250},
            {"pot", 251},
            {"rue", 252},
            {"got", 253},
            {"bye", 254},
            {"fed", 255}

    };



        #endregion

        #region IDisposable

        public void Dispose()
        {
            // do nothing
        }

        #endregion

        #region public

        public Task InitializeAsync(IDnsServer dnsServer, string config)
        {
            _dnsServer = dnsServer;

            return Task.CompletedTask;
        }

        public byte? Translate(string word)
        {
            if (ipw4map.TryGetValue(word.ToLower(), out byte value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public async Task<DnsDatagram> ProcessRequestAsync(DnsDatagram request, IPEndPoint remoteEP, DnsTransportProtocol protocol, bool isRecursionAllowed, string zoneName, string appRecordName, uint appRecordTtl, string appRecordData)
        {
            string qname = request.Question[0].Name;

            if (qname.Length == appRecordName.Length)
                return null;

            DnsResourceRecord answer = null;

            if (request.Question[0].Type == DnsResourceRecordType.A)
            {
                string subdomain = qname.Substring(0, qname.Length - appRecordName.Length);
                string[] parts = subdomain.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 4)
                {
                    var start = parts.Length - 4;
                    parts = parts[start..];
                }

                if (parts.Length == 4)
                {
                    byte[] rawIp = new byte[4];
                    int i = 0;
                    foreach (string part in parts)
                    {
                        var translated = Translate(part);
                        if (translated is null)
                            continue;
                        rawIp[i++] = translated.Value;
                    }

                    if (i == 4)
                    {
                        answer = new DnsResourceRecord(request.Question[0].Name, DnsResourceRecordType.A, DnsClass.IN, appRecordTtl, new DnsARecordData(new IPAddress(rawIp)));
                    }
                }
            }

            if (answer is null)
            {
                //NODATA reponse
                DnsDatagram soaResponse = await _dnsServer.DirectQueryAsync(new DnsQuestionRecord(zoneName, DnsResourceRecordType.SOA, DnsClass.IN));

                return new DnsDatagram(request.Identifier, true, DnsOpcode.StandardQuery, true, false, request.RecursionDesired, isRecursionAllowed, false, false, DnsResponseCode.NoError, request.Question, null, soaResponse.Answer);
            }

            return new DnsDatagram(request.Identifier, true, request.OPCODE, true, false, request.RecursionDesired, isRecursionAllowed, false, false, DnsResponseCode.NoError, request.Question, new DnsResourceRecord[] { answer });
        }

        #endregion

        #region properties

        public string Description
        { get { return "Returns the IP address for a given IPw4 name. Works similarily to sslip/wild ip but with words instead of numbers."; } }

        public string ApplicationRecordDataTemplate
        { get { return null; } }

        #endregion
    }
}