using System.Collections.Generic;

namespace EasySoft.UtilityTools.Mime
{
    /// <summary>
    /// MimeCollection
    /// </summary>
    public class MimeCollection
    {
        /// <summary>
        /// Any mime：.*=application/octet-stream
        /// </summary>
        public static MimeModel Any => new()
        {
            Name = "Any",
            Extension = ".*",
            ContentType = "application/octet-stream",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Status001 mime：.001=application/x-001
        /// </summary>
        public static MimeModel Status001 => new()
        {
            Name = "Status001",
            Extension = ".001",
            ContentType = "application/x-001",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Status301 mime：.301=application/x-301
        /// </summary>
        public static MimeModel Status301 => new()
        {
            Name = "Status301",
            Extension = ".301",
            ContentType = "application/x-301",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Status323 mime：.323=text/h323
        /// </summary>
        public static MimeModel Status323 => new()
        {
            Name = "Status323",
            Extension = ".323",
            ContentType = "text/h323",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Status906 mime：.906=application/x-906
        /// </summary>
        public static MimeModel Status906 => new()
        {
            Name = "Status906",
            Extension = ".906",
            ContentType = "application/x-906",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Status907 mime：.907=drawing/907
        /// </summary>
        public static MimeModel Status907 => new()
        {
            Name = "Status907",
            Extension = ".907",
            ContentType = "drawing/907",
            Alias = new List<string>(),
        };

        /// <summary>
        /// A11 mime：.a11=application/x-a11
        /// </summary>
        public static MimeModel A11 => new()
        {
            Name = "A11",
            Extension = ".a11",
            ContentType = "application/x-a11",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Acp mime：.acp=audio/x-mei-aac
        /// </summary>
        public static MimeModel Acp => new()
        {
            Name = "Acp",
            Extension = ".acp",
            ContentType = "audio/x-mei-aac",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ai mime：.ai=application/postscript
        /// </summary>
        public static MimeModel Ai => new()
        {
            Name = "Ai",
            Extension = ".ai",
            ContentType = "application/postscript",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Aif mime：.aif=audio/aiff
        /// </summary>
        public static MimeModel Aif => new()
        {
            Name = "Aif",
            Extension = ".aif",
            ContentType = "audio/aiff",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Aifc mime：.aifc=audio/aiff
        /// </summary>
        public static MimeModel Aifc => new()
        {
            Name = "Aifc",
            Extension = ".aifc",
            ContentType = "audio/aiff",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Aiff mime：.aiff=audio/aiff
        /// </summary>
        public static MimeModel Aiff => new()
        {
            Name = "Aiff",
            Extension = ".aiff",
            ContentType = "audio/aiff",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Anv mime：.anv=application/x-anv
        /// </summary>
        public static MimeModel Anv => new()
        {
            Name = "Anv",
            Extension = ".anv",
            ContentType = "application/x-anv",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Asa mime：.asa=text/asa
        /// </summary>
        public static MimeModel Asa => new()
        {
            Name = "Asa",
            Extension = ".asa",
            ContentType = "text/asa",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Asf mime：.asf=video/x-ms-asf
        /// </summary>
        public static MimeModel Asf => new()
        {
            Name = "Asf",
            Extension = ".asf",
            ContentType = "video/x-ms-asf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Asp mime：.asp=text/asp
        /// </summary>
        public static MimeModel Asp => new()
        {
            Name = "Asp",
            Extension = ".asp",
            ContentType = "text/asp",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Asx mime：.asx=video/x-ms-asf
        /// </summary>
        public static MimeModel Asx => new()
        {
            Name = "Asx",
            Extension = ".asx",
            ContentType = "video/x-ms-asf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Au mime：.au=audio/basic
        /// </summary>
        public static MimeModel Au => new()
        {
            Name = "Au",
            Extension = ".au",
            ContentType = "audio/basic",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Avi mime：.avi=video/avi
        /// </summary>
        public static MimeModel Avi => new()
        {
            Name = "Avi",
            Extension = ".avi",
            ContentType = "video/avi",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Awf mime：.awf=application/vnd.adobe.workflow
        /// </summary>
        public static MimeModel Awf => new()
        {
            Name = "Awf",
            Extension = ".awf",
            ContentType = "application/vnd.adobe.workflow",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Biz mime：.biz=text/xml
        /// </summary>
        public static MimeModel Biz => new()
        {
            Name = "Biz",
            Extension = ".biz",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Bmp mime：.bmp=application/x-bmp
        /// </summary>
        public static MimeModel Bmp => new()
        {
            Name = "Bmp",
            Extension = ".bmp",
            ContentType = "application/x-bmp",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Bot mime：.bot=application/x-bot
        /// </summary>
        public static MimeModel Bot => new()
        {
            Name = "Bot",
            Extension = ".bot",
            ContentType = "application/x-bot",
            Alias = new List<string>(),
        };

        /// <summary>
        /// C4t mime：.c4t=application/x-c4t
        /// </summary>
        public static MimeModel C4T => new()
        {
            Name = "C4t",
            Extension = ".c4t",
            ContentType = "application/x-c4t",
            Alias = new List<string>(),
        };

        /// <summary>
        /// C90 mime：.c90=application/x-c90
        /// </summary>
        public static MimeModel C90 => new()
        {
            Name = "C90",
            Extension = ".c90",
            ContentType = "application/x-c90",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cal mime：.cal=application/x-cals
        /// </summary>
        public static MimeModel Cal => new()
        {
            Name = "Cal",
            Extension = ".cal",
            ContentType = "application/x-cals",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cat mime：.cat=application/s-pki.seccat
        /// </summary>
        public static MimeModel Cat => new()
        {
            Name = "Cat",
            Extension = ".cat",
            ContentType = "application/s-pki.seccat",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cdf mime：.cdf=application/x-netcdf
        /// </summary>
        public static MimeModel Cdf => new()
        {
            Name = "Cdf",
            Extension = ".cdf",
            ContentType = "application/x-netcdf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cdr mime：.cdr=application/x-cdr
        /// </summary>
        public static MimeModel Cdr => new()
        {
            Name = "Cdr",
            Extension = ".cdr",
            ContentType = "application/x-cdr",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cel mime：.cel=application/x-cel
        /// </summary>
        public static MimeModel Cel => new()
        {
            Name = "Cel",
            Extension = ".cel",
            ContentType = "application/x-cel",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cer mime：.cer=application/x-x509-ca-cert
        /// </summary>
        public static MimeModel Cer => new()
        {
            Name = "Cer",
            Extension = ".cer",
            ContentType = "application/x-x509-ca-cert",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cg4 mime：.cg4=application/x-g4
        /// </summary>
        public static MimeModel Cg4 => new()
        {
            Name = "Cg4",
            Extension = ".cg4",
            ContentType = "application/x-g4",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cgm mime：.cgm=application/x-cgm
        /// </summary>
        public static MimeModel Cgm => new()
        {
            Name = "Cgm",
            Extension = ".cgm",
            ContentType = "application/x-cgm",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cit mime：.cit=application/x-cit
        /// </summary>
        public static MimeModel Cit => new()
        {
            Name = "Cit",
            Extension = ".cit",
            ContentType = "application/x-cit",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Class mime：.class=java/*
        /// </summary>
        public static MimeModel Class => new()
        {
            Name = "Class",
            Extension = ".class",
            ContentType = "java/*",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cml mime：.cml=text/xml
        /// </summary>
        public static MimeModel Cml => new()
        {
            Name = "Cml",
            Extension = ".cml",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cmp mime：.cmp=application/x-cmp
        /// </summary>
        public static MimeModel Cmp => new()
        {
            Name = "Cmp",
            Extension = ".cmp",
            ContentType = "application/x-cmp",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cmx mime：.cmx=application/x-cmx
        /// </summary>
        public static MimeModel Cmx => new()
        {
            Name = "Cmx",
            Extension = ".cmx",
            ContentType = "application/x-cmx",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cot mime：.cot=application/x-cot
        /// </summary>
        public static MimeModel Cot => new()
        {
            Name = "Cot",
            Extension = ".cot",
            ContentType = "application/x-cot",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Crl mime：.crl=application/pkix-crl
        /// </summary>
        public static MimeModel Crl => new()
        {
            Name = "Crl",
            Extension = ".crl",
            ContentType = "application/pkix-crl",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Crt mime：.crt=application/x-x509-ca-cert
        /// </summary>
        public static MimeModel Crt => new()
        {
            Name = "Crt",
            Extension = ".crt",
            ContentType = "application/x-x509-ca-cert",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Csi mime：.csi=application/x-csi
        /// </summary>
        public static MimeModel Csi => new()
        {
            Name = "Csi",
            Extension = ".csi",
            ContentType = "application/x-csi",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Css mime：.css=text/css
        /// </summary>
        public static MimeModel Css => new()
        {
            Name = "Css",
            Extension = ".css",
            ContentType = "text/css",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Cut mime：.cut=application/x-cut
        /// </summary>
        public static MimeModel Cut => new()
        {
            Name = "Cut",
            Extension = ".cut",
            ContentType = "application/x-cut",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Dbf mime：.dbf=application/x-dbf
        /// </summary>
        public static MimeModel Dbf => new()
        {
            Name = "Dbf",
            Extension = ".dbf",
            ContentType = "application/x-dbf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Dbm mime：.dbm=application/x-dbm
        /// </summary>
        public static MimeModel Dbm => new()
        {
            Name = "Dbm",
            Extension = ".dbm",
            ContentType = "application/x-dbm",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Dbx mime：.dbx=application/x-dbx
        /// </summary>
        public static MimeModel Dbx => new()
        {
            Name = "Dbx",
            Extension = ".dbx",
            ContentType = "application/x-dbx",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Dcd mime：.dcd=text/xml
        /// </summary>
        public static MimeModel Dcd => new()
        {
            Name = "Dcd",
            Extension = ".dcd",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Dcx mime：.dcx=application/x-dcx
        /// </summary>
        public static MimeModel Dcx => new()
        {
            Name = "Dcx",
            Extension = ".dcx",
            ContentType = "application/x-dcx",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Der mime：.der=application/x-x509-ca-cert
        /// </summary>
        public static MimeModel Der => new()
        {
            Name = "Der",
            Extension = ".der",
            ContentType = "application/x-x509-ca-cert",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Dgn mime：.dgn=application/x-dgn
        /// </summary>
        public static MimeModel Dgn => new()
        {
            Name = "Dgn",
            Extension = ".dgn",
            ContentType = "application/x-dgn",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Dib mime：.dib=application/x-dib
        /// </summary>
        public static MimeModel Dib => new()
        {
            Name = "Dib",
            Extension = ".dib",
            ContentType = "application/x-dib",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Dll mime：.dll=application/x-msdownload
        /// </summary>
        public static MimeModel Dll => new()
        {
            Name = "Dll",
            Extension = ".dll",
            ContentType = "application/x-msdownload",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Doc mime：.doc=application/msword
        /// </summary>
        public static MimeModel Doc => new()
        {
            Name = "Doc",
            Extension = ".doc",
            ContentType = "application/msword",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Dot mime：.dot=application/msword
        /// </summary>
        public static MimeModel Dot => new()
        {
            Name = "Dot",
            Extension = ".dot",
            ContentType = "application/msword",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Drw mime：.drw=application/x-drw
        /// </summary>
        public static MimeModel Drw => new()
        {
            Name = "Drw",
            Extension = ".drw",
            ContentType = "application/x-drw",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Dtd mime：.dtd=text/xml
        /// </summary>
        public static MimeModel Dtd => new()
        {
            Name = "Dtd",
            Extension = ".dtd",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Dwf mime：.dwf=Model/vnd.dwf， .dwf=application/x-dwf
        /// </summary>
        public static MimeModel Dwf => new()
        {
            Name = "Dwf",
            Extension = ".dwf",
            ContentType = "Model/vnd.dwf",
            Alias = new List<string>
            {
                "application/x-dwf",
            },
        };

        /// <summary>
        /// Dwg mime：.dwg=application/x-dwg
        /// </summary>
        public static MimeModel Dwg => new()
        {
            Name = "Dwg",
            Extension = ".dwg",
            ContentType = "application/x-dwg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Dxb mime：.dxb=application/x-dxb
        /// </summary>
        public static MimeModel Dxb => new()
        {
            Name = "Dxb",
            Extension = ".dxb",
            ContentType = "application/x-dxb",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Dxf mime：.dxf=application/x-dxf
        /// </summary>
        public static MimeModel Dxf => new()
        {
            Name = "Dxf",
            Extension = ".dxf",
            ContentType = "application/x-dxf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Edn mime：.edn=application/vnd.adobe.edn
        /// </summary>
        public static MimeModel Edn => new()
        {
            Name = "Edn",
            Extension = ".edn",
            ContentType = "application/vnd.adobe.edn",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Emf mime：.emf=application/x-emf
        /// </summary>
        public static MimeModel Emf => new()
        {
            Name = "Emf",
            Extension = ".emf",
            ContentType = "application/x-emf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Eml mime：.eml=message/rfc822
        /// </summary>
        public static MimeModel Eml => new()
        {
            Name = "Eml",
            Extension = ".eml",
            ContentType = "message/rfc822",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ent mime：.ent=text/xml
        /// </summary>
        public static MimeModel Ent => new()
        {
            Name = "Ent",
            Extension = ".ent",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Epi mime：.epi=application/x-epi
        /// </summary>
        public static MimeModel Epi => new()
        {
            Name = "Epi",
            Extension = ".epi",
            ContentType = "application/x-epi",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Eps mime：.eps=application/x-ps，.eps=application/postscript
        /// </summary>
        public static MimeModel Eps => new()
        {
            Name = "Eps",
            Extension = ".eps",
            ContentType = "application/x-ps",
            Alias = new List<string>
            {
                "application/postscript"
            },
        };

        /// <summary>
        /// Etd mime：.etd=application/x-ebx
        /// </summary>
        public static MimeModel Etd => new()
        {
            Name = "Etd",
            Extension = ".etd",
            ContentType = "application/x-ebx",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Exe mime：.exe=application/x-msdownload
        /// </summary>
        public static MimeModel Exe => new()
        {
            Name = "Exe",
            Extension = ".exe",
            ContentType = "application/x-msdownload",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Fax mime：.fax=image/fax
        /// </summary>
        public static MimeModel Fax => new()
        {
            Name = "Fax",
            Extension = ".fax",
            ContentType = "image/fax",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Fdf mime：.fdf=application/vnd.fdf
        /// </summary>
        public static MimeModel Fdf => new()
        {
            Name = "Fdf",
            Extension = ".fdf",
            ContentType = "application/vnd.fdf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Fif mime：.fif=application/fractals
        /// </summary>
        public static MimeModel Fif => new()
        {
            Name = "Fif",
            Extension = ".fif",
            ContentType = "application/fractals",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Fo mime：.fo=text/xml
        /// </summary>
        public static MimeModel Fo => new()
        {
            Name = "Fo",
            Extension = ".fo",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Frm mime：.frm=application/x-frm
        /// </summary>
        public static MimeModel Frm => new()
        {
            Name = "Frm",
            Extension = ".frm",
            ContentType = "application/x-frm",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Frm mime：application/x-www-form-urlencoded
        /// </summary>
        public static MimeModel FormUrlencoded => new()
        {
            Name = "FormUrlencoded",
            Extension = "",
            ContentType = "application/x-www-form-urlencoded",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Frm mime：multipart/form-data
        /// </summary>
        public static MimeModel FormData => new()
        {
            Name = "FormData",
            Extension = "",
            ContentType = "multipart/form-data",
            Alias = new List<string>(),
        };

        /// <summary>
        /// G4 mime：.g4=application/x-g4
        /// </summary>
        public static MimeModel G4 => new()
        {
            Name = "G4",
            Extension = ".g4",
            ContentType = "application/x-g4",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Gbr mime：.gbr=application/x-gbr
        /// </summary>
        public static MimeModel Gbr => new()
        {
            Name = "Gbr",
            Extension = ".gbr",
            ContentType = "application/x-gbr",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Gcd mime：.gcd=application/x-gcd
        /// </summary>
        public static MimeModel Gcd => new()
        {
            Name = "Gcd",
            Extension = ".gcd",
            ContentType = "application/x-gcd",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Gif mime：.gif=image/gif
        /// </summary>
        public static MimeModel Gif => new()
        {
            Name = "Gif",
            Extension = ".gif",
            ContentType = "image/gif",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Gl2 mime：.gl2=application/x-gl2
        /// </summary>
        public static MimeModel Gl2 => new()
        {
            Name = "Gl2",
            Extension = ".gl2",
            ContentType = "application/x-gl2",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Gp4 mime：.gp4=application/x-gp4
        /// </summary>
        public static MimeModel Gp4 => new()
        {
            Name = "Gp4",
            Extension = ".gp4",
            ContentType = "application/x-gp4",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Hgl mime：.hgl=application/x-hgl
        /// </summary>
        public static MimeModel Hgl => new()
        {
            Name = "Hgl",
            Extension = ".hgl",
            ContentType = "application/x-hgl",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Hmr mime：.hmr=application/x-hmr
        /// </summary>
        public static MimeModel Hmr => new()
        {
            Name = "Hmr",
            Extension = ".hmr",
            ContentType = "application/x-hmr",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Hpg mime：.hpg=application/x-hpgl
        /// </summary>
        public static MimeModel Hpg => new()
        {
            Name = "Hpg",
            Extension = ".hpg",
            ContentType = "application/x-hpgl",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Hpl mime：.hpl=application/x-hpl
        /// </summary>
        public static MimeModel Hpl => new()
        {
            Name = "Hpl",
            Extension = ".hpl",
            ContentType = "application/x-hpl",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Hqx mime：.hqx=application/mac-binhex40
        /// </summary>
        public static MimeModel Hqx => new()
        {
            Name = "Hqx",
            Extension = ".hqx",
            ContentType = "application/mac-binhex40",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Hrf mime：.hrf=application/x-hrf
        /// </summary>
        public static MimeModel Hrf => new()
        {
            Name = "Hrf",
            Extension = ".hrf",
            ContentType = "application/x-hrf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Hta mime：.hta=application/hta
        /// </summary>
        public static MimeModel Hta => new()
        {
            Name = "Hta",
            Extension = ".hta",
            ContentType = "application/hta",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Htc mime：.htc=text/x-component
        /// </summary>
        public static MimeModel Htc => new()
        {
            Name = "Htc",
            Extension = ".htc",
            ContentType = "text/x-component",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Htm mime：.htm=text/html
        /// </summary>
        public static MimeModel Htm => new()
        {
            Name = "Htm",
            Extension = ".htm",
            ContentType = "text/html",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Html mime：.html=text/html
        /// </summary>
        public static MimeModel Html => new()
        {
            Name = "Html",
            Extension = ".html",
            ContentType = "text/html",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Htt mime：.htt=text/webviewhtml
        /// </summary>
        public static MimeModel Htt => new()
        {
            Name = "Htt",
            Extension = ".htt",
            ContentType = "text/webviewhtml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Htx mime：.htx=text/html
        /// </summary>
        public static MimeModel Htx => new()
        {
            Name = "Htx",
            Extension = ".htx",
            ContentType = "text/html",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Icb mime：.icb=application/x-icb
        /// </summary>
        public static MimeModel Icb => new()
        {
            Name = "Icb",
            Extension = ".icb",
            ContentType = "application/x-icb",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ico mime：.ico=image/x-icon，.ico=application/x-ico
        /// </summary>
        public static MimeModel Ico => new()
        {
            Name = "Ico",
            Extension = ".ico",
            ContentType = "image/x-icon",
            Alias = new List<string>
            {
                "application/x-ico",
            },
        };

        /// <summary>
        /// Iff mime：.iff=application/x-iff
        /// </summary>
        public static MimeModel Iff => new()
        {
            Name = "Iff",
            Extension = ".iff",
            ContentType = "application/x-iff",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ig4 mime：.ig4=application/x-g4
        /// </summary>
        public static MimeModel Ig4 => new()
        {
            Name = "Ig4",
            Extension = ".ig4",
            ContentType = "application/x-g4",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Igs mime：.igs=application/x-igs
        /// </summary>
        public static MimeModel Igs => new()
        {
            Name = "Igs",
            Extension = ".igs",
            ContentType = "application/x-igs",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Iii mime：.iii=application/x-iphone
        /// </summary>
        public static MimeModel Iii => new()
        {
            Name = "Iii",
            Extension = ".iii",
            ContentType = "application/x-iphone",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Img mime：.img=application/x-img
        /// </summary>
        public static MimeModel Img => new()
        {
            Name = "Img",
            Extension = ".img",
            ContentType = "application/x-img",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ins mime：.ins=application/x-internet-signup
        /// </summary>
        public static MimeModel Ins => new()
        {
            Name = "Ins",
            Extension = ".ins",
            ContentType = "application/x-internet-signup",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Isp mime：.isp=application/x-internet-signup
        /// </summary>
        public static MimeModel Isp => new()
        {
            Name = "Isp",
            Extension = ".isp",
            ContentType = "application/x-internet-signup",
            Alias = new List<string>(),
        };

        /// <summary>
        /// IVF mime：.IVF=video/x-ivf
        /// </summary>
        public static MimeModel Ivf => new()
        {
            Name = "IVF",
            Extension = ".IVF",
            ContentType = "video/x-ivf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Java mime：.java=java/*
        /// </summary>
        public static MimeModel Java => new()
        {
            Name = "Java",
            Extension = ".java",
            ContentType = "java/*",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Jfif mime：.jfif=image/jpeg
        /// </summary>
        public static MimeModel Jfif => new()
        {
            Name = "Jfif",
            Extension = ".jfif",
            ContentType = "image/jpeg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Jpe mime：.jpe=image/jpeg，.jpe=application/x-jpe
        /// </summary>
        public static MimeModel Jpe => new()
        {
            Name = "Jpe",
            Extension = ".jpe",
            ContentType = "image/jpeg",
            Alias = new List<string>
            {
                "application/x-jpe",
            },
        };

        /// <summary>
        /// Jpeg mime：.jpeg=image/jpeg，.jpg=image/jpeg
        /// </summary>
        public static MimeModel Jpeg => new()
        {
            Name = "Jpeg",
            Extension = ".jpeg",
            ContentType = "image/jpeg",
            Alias = new List<string>
            {
                "image/jpeg",
            },
        };

        /// <summary>
        /// Jpg mime：.jpg=application/x-jpg
        /// </summary>
        public static MimeModel Jpg => new()
        {
            Name = "Jpg",
            Extension = ".jpg",
            ContentType = "application/x-jpg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Js mime：.js=application/x-javascript
        /// </summary>
        public static MimeModel Js => new()
        {
            Name = "Js",
            Extension = ".js",
            ContentType = "application/x-javascript",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Jsp mime：.jsp=text/html
        /// </summary>
        public static MimeModel Jsp => new()
        {
            Name = "Jsp",
            Extension = ".jsp",
            ContentType = "text/html",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Json mime：.js=application/json
        /// </summary>
        public static MimeModel Json => new()
        {
            Name = "Json",
            Extension = ".js",
            ContentType = "application/json",
            Alias = new List<string>(),
        };

        /// <summary>
        /// La1 mime：.la1=audio/x-liquid-file
        /// </summary>
        public static MimeModel La1 => new()
        {
            Name = "La1",
            Extension = ".la1",
            ContentType = "audio/x-liquid-file",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Lar mime：.lar=application/x-laplayer-reg
        /// </summary>
        public static MimeModel Lar => new()
        {
            Name = "Lar",
            Extension = ".lar",
            ContentType = "application/x-laplayer-reg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Latex mime：.latex=application/x-latex
        /// </summary>
        public static MimeModel Latex => new()
        {
            Name = "Latex",
            Extension = ".latex",
            ContentType = "application/x-latex",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Lavs mime：.lavs=audio/x-liquid-secure
        /// </summary>
        public static MimeModel Lavs => new()
        {
            Name = "Lavs",
            Extension = ".lavs",
            ContentType = "audio/x-liquid-secure",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Lbm mime：.lbm=application/x-lbm
        /// </summary>
        public static MimeModel Lbm => new()
        {
            Name = "Lbm",
            Extension = ".lbm",
            ContentType = "application/x-lbm",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Lmsff mime：.lmsff=audio/x-la-lms
        /// </summary>
        public static MimeModel Lmsff => new()
        {
            Name = "Lmsff",
            Extension = ".lmsff",
            ContentType = "audio/x-la-lms",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ls mime：.ls=application/x-javascript
        /// </summary>
        public static MimeModel Ls => new()
        {
            Name = "Ls",
            Extension = ".ls",
            ContentType = "application/x-javascript",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ltr mime：.ltr=application/x-ltr
        /// </summary>
        public static MimeModel Ltr => new()
        {
            Name = "Ltr",
            Extension = ".ltr",
            ContentType = "application/x-ltr",
            Alias = new List<string>(),
        };

        /// <summary>
        /// M1v mime：.m1v=video/x-mpeg
        /// </summary>
        public static MimeModel M1V => new()
        {
            Name = "M1v",
            Extension = ".m1v",
            ContentType = "video/x-mpeg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// M2v mime：.m2v=video/x-mpeg
        /// </summary>
        public static MimeModel M2V => new()
        {
            Name = "M2v",
            Extension = ".m2v",
            ContentType = "video/x-mpeg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// M3u mime：.m3u=audio/mpegurl
        /// </summary>
        public static MimeModel M3U => new()
        {
            Name = "M3u",
            Extension = ".m3u",
            ContentType = "audio/mpegurl",
            Alias = new List<string>(),
        };

        /// <summary>
        /// M4e mime：.m4e=video/mpeg4
        /// </summary>
        public static MimeModel M4E => new()
        {
            Name = "M4e",
            Extension = ".m4e",
            ContentType = "video/mpeg4",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mac mime：.mac=application/x-mac
        /// </summary>
        public static MimeModel Mac => new()
        {
            Name = "Mac",
            Extension = ".mac",
            ContentType = "application/x-mac",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Man mime：.man=application/x-troff-man
        /// </summary>
        public static MimeModel Man => new()
        {
            Name = "Man",
            Extension = ".man",
            ContentType = "application/x-troff-man",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Math mime：.math=text/xml
        /// </summary>
        public static MimeModel Math => new()
        {
            Name = "Math",
            Extension = ".math",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mdb mime：.mdb=application/msaccess，.mdb=application/x-mdb
        /// </summary>
        public static MimeModel Mdb => new()
        {
            Name = "Mdb",
            Extension = ".mdb",
            ContentType = "application/msaccess",
            Alias = new List<string>
            {
                "application/x-mdb",
            },
        };

        /// <summary>
        /// Mfp mime：.mfp=application/x-shockwave-flash
        /// </summary>
        public static MimeModel Mfp => new()
        {
            Name = "Mfp",
            Extension = ".mfp",
            ContentType = "application/x-shockwave-flash",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mht mime：.mht=message/rfc822
        /// </summary>
        public static MimeModel Mht => new()
        {
            Name = "Mht",
            Extension = ".mht",
            ContentType = "message/rfc822",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mhtml mime：.mhtml=message/rfc822
        /// </summary>
        public static MimeModel Mhtml => new()
        {
            Name = "Mhtml",
            Extension = ".mhtml",
            ContentType = "message/rfc822",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mi mime：.mi=application/x-mi
        /// </summary>
        public static MimeModel Mi => new()
        {
            Name = "Mi",
            Extension = ".mi",
            ContentType = "application/x-mi",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mid mime：.mid=audio/mid
        /// </summary>
        public static MimeModel Mid => new()
        {
            Name = "Mid",
            Extension = ".mid",
            ContentType = "audio/mid",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Midi mime：.midi=audio/mid
        /// </summary>
        public static MimeModel Midi => new()
        {
            Name = "Midi",
            Extension = ".midi",
            ContentType = "audio/mid",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mil mime：.mil=application/x-mil
        /// </summary>
        public static MimeModel Mil => new()
        {
            Name = "Mil",
            Extension = ".mil",
            ContentType = "application/x-mil",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mml mime：.mml=text/xml
        /// </summary>
        public static MimeModel Mml => new()
        {
            Name = "Mml",
            Extension = ".mml",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mnd mime：.mnd=audio/x-musicnet-download
        /// </summary>
        public static MimeModel Mnd => new()
        {
            Name = "Mnd",
            Extension = ".mnd",
            ContentType = "audio/x-musicnet-download",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mns mime：.mns=audio/x-musicnet-stream
        /// </summary>
        public static MimeModel Mns => new()
        {
            Name = "Mns",
            Extension = ".mns",
            ContentType = "audio/x-musicnet-stream",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mocha mime：.mocha=application/x-javascript
        /// </summary>
        public static MimeModel Mocha => new()
        {
            Name = "Mocha",
            Extension = ".mocha",
            ContentType = "application/x-javascript",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Movie mime：.movie=video/x-sgi-movie
        /// </summary>
        public static MimeModel Movie => new()
        {
            Name = "Movie",
            Extension = ".movie",
            ContentType = "video/x-sgi-movie",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mp1 mime：.mp1=audio/mp1
        /// </summary>
        public static MimeModel Mp1 => new()
        {
            Name = "Mp1",
            Extension = ".mp1",
            ContentType = "audio/mp1",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mp2 mime：.mp2=audio/mp2
        /// </summary>
        public static MimeModel Mp2 => new()
        {
            Name = "Mp2",
            Extension = ".mp2",
            ContentType = "audio/mp2",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mp2v mime：.mp2v=video/mpeg
        /// </summary>
        public static MimeModel Mp2V => new()
        {
            Name = "Mp2v",
            Extension = ".mp2v",
            ContentType = "video/mpeg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mp3 mime：.mp3=audio/mp3
        /// </summary>
        public static MimeModel Mp3 => new()
        {
            Name = "Mp3",
            Extension = ".mp3",
            ContentType = "audio/mp3",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mp4 mime：.mp4=video/mp4
        /// </summary>
        public static MimeModel Mp4 => new()
        {
            Name = "Mp4",
            Extension = ".mp4",
            ContentType = "video/mp4",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mpa mime：.mpa=video/x-mpg
        /// </summary>
        public static MimeModel Mpa => new()
        {
            Name = "Mpa",
            Extension = ".mpa",
            ContentType = "video/x-mpg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mpd mime：.mpd=application/-project
        /// </summary>
        public static MimeModel Mpd => new()
        {
            Name = "Mpd",
            Extension = ".mpd",
            ContentType = "application/-project",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mpe mime：.mpe=video/x-mpeg
        /// </summary>
        public static MimeModel Mpe => new()
        {
            Name = "Mpe",
            Extension = ".mpe",
            ContentType = "video/x-mpeg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mpeg mime：.mpeg=video/mpg
        /// </summary>
        public static MimeModel Mpeg => new()
        {
            Name = "Mpeg",
            Extension = ".mpeg",
            ContentType = "video/mpg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mpg mime：.mpg=video/mpg
        /// </summary>
        public static MimeModel Mpg => new()
        {
            Name = "Mpg",
            Extension = ".mpg",
            ContentType = "video/mpg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mpga mime：.mpga=audio/rn-mpeg
        /// </summary>
        public static MimeModel Mpga => new()
        {
            Name = "Mpga",
            Extension = ".mpga",
            ContentType = "audio/rn-mpeg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mpp mime：.mpp=application/-project
        /// </summary>
        public static MimeModel Mpp => new()
        {
            Name = "Mpp",
            Extension = ".mpp",
            ContentType = "application/-project",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mps mime：.mps=video/x-mpeg
        /// </summary>
        public static MimeModel Mps => new()
        {
            Name = "Mps",
            Extension = ".mps",
            ContentType = "video/x-mpeg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mpt mime：.mpt=application/-project
        /// </summary>
        public static MimeModel Mpt => new()
        {
            Name = "Mpt",
            Extension = ".mpt",
            ContentType = "application/-project",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mpv mime：.mpv=video/mpg
        /// </summary>
        public static MimeModel Mpv => new()
        {
            Name = "Mpv",
            Extension = ".mpv",
            ContentType = "video/mpg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mpv2 mime：.mpv2=video/mpeg
        /// </summary>
        public static MimeModel Mpv2 => new()
        {
            Name = "Mpv2",
            Extension = ".mpv2",
            ContentType = "video/mpeg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mpw mime：.mpw=application/s-project
        /// </summary>
        public static MimeModel Mpw => new()
        {
            Name = "Mpw",
            Extension = ".mpw",
            ContentType = "application/s-project",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mpx mime：.mpx=application/-project
        /// </summary>
        public static MimeModel Mpx => new()
        {
            Name = "Mpx",
            Extension = ".mpx",
            ContentType = "application/-project",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mtx mime：.mtx=text/xml
        /// </summary>
        public static MimeModel Mtx => new()
        {
            Name = "Mtx",
            Extension = ".mtx",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Mxp mime：.mxp=application/x-mmxp
        /// </summary>
        public static MimeModel Mxp => new()
        {
            Name = "Mxp",
            Extension = ".mxp",
            ContentType = "application/x-mmxp",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Net mime：.net=image/pnetvue
        /// </summary>
        public static MimeModel Net => new()
        {
            Name = "Net",
            Extension = ".net",
            ContentType = "image/pnetvue",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Nrf mime：.nrf=application/x-nrf
        /// </summary>
        public static MimeModel Nrf => new()
        {
            Name = "Nrf",
            Extension = ".nrf",
            ContentType = "application/x-nrf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Nws mime：.nws=message/rfc822
        /// </summary>
        public static MimeModel Nws => new()
        {
            Name = "Nws",
            Extension = ".nws",
            ContentType = "message/rfc822",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Odc mime：.odc=text/x-ms-odc
        /// </summary>
        public static MimeModel Odc => new()
        {
            Name = "Odc",
            Extension = ".odc",
            ContentType = "text/x-ms-odc",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Out mime：.out=application/x-out
        /// </summary>
        public static MimeModel Out => new()
        {
            Name = "Out",
            Extension = ".out",
            ContentType = "application/x-out",
            Alias = new List<string>(),
        };

        /// <summary>
        /// P10 mime：.p10=application/pkcs10
        /// </summary>
        public static MimeModel P10 => new()
        {
            Name = "P10",
            Extension = ".p10",
            ContentType = "application/pkcs10",
            Alias = new List<string>(),
        };

        /// <summary>
        /// P12 mime：.p12=application/x-pkcs12
        /// </summary>
        public static MimeModel P12 => new()
        {
            Name = "P12",
            Extension = ".p12",
            ContentType = "application/x-pkcs12",
            Alias = new List<string>(),
        };

        /// <summary>
        /// P7b mime：.p7b=application/x-pkcs7-certificates
        /// </summary>
        public static MimeModel P7B => new()
        {
            Name = "P7B",
            Extension = ".p7b",
            ContentType = "application/x-pkcs7-certificates",
            Alias = new List<string>(),
        };

        /// <summary>
        /// P7c mime：.p7c=application/pkcs7-mime
        /// </summary>
        public static MimeModel P7C => new()
        {
            Name = "P7C",
            Extension = ".p7c",
            ContentType = "application/pkcs7-mime",
            Alias = new List<string>(),
        };

        /// <summary>
        /// P7m mime：.p7m=application/pkcs7-mime
        /// </summary>
        public static MimeModel P7M => new()
        {
            Name = "P7M",
            Extension = ".p7m",
            ContentType = "application/pkcs7-mime",
            Alias = new List<string>(),
        };

        /// <summary>
        /// P7r mime：.p7r=application/x-pkcs7-certreqresp
        /// </summary>
        public static MimeModel P7R => new()
        {
            Name = "P7R",
            Extension = ".p7r",
            ContentType = "application/x-pkcs7-certreqresp",
            Alias = new List<string>(),
        };

        /// <summary>
        /// P7s mime：.p7s=application/pkcs7-signature
        /// </summary>
        public static MimeModel P7S => new()
        {
            Name = "P7S",
            Extension = ".p7s",
            ContentType = "application/pkcs7-signature",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pc5 mime：.pc5=application/x-pc5
        /// </summary>
        public static MimeModel Pc5 => new()
        {
            Name = "Pc5",
            Extension = ".pc5",
            ContentType = "application/x-pc5",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pci mime：.pci=application/x-pci
        /// </summary>
        public static MimeModel Pci => new()
        {
            Name = "Pci",
            Extension = ".pci",
            ContentType = "application/x-pci",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pcl mime：.pcl=application/x-pcl
        /// </summary>
        public static MimeModel Pcl => new()
        {
            Name = "Pcl",
            Extension = ".pcl",
            ContentType = "application/x-pcl",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pcx mime：.pcx=application/x-pcx
        /// </summary>
        public static MimeModel Pcx => new()
        {
            Name = "Pcx",
            Extension = ".pcx",
            ContentType = "application/x-pcx",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pdf mime：.pdf=application/pdf
        /// </summary>
        public static MimeModel Pdf => new()
        {
            Name = "Pdf",
            Extension = ".pdf",
            ContentType = "application/pdf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pdx mime：.pdx=application/vnd.adobe.pdx
        /// </summary>
        public static MimeModel Pdx => new()
        {
            Name = "Pdx",
            Extension = ".pdx",
            ContentType = "application/vnd.adobe.pdx",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pfx mime：.pfx=application/x-pkcs12
        /// </summary>
        public static MimeModel Pfx => new()
        {
            Name = "Pfx",
            Extension = ".pfx",
            ContentType = "application/x-pkcs12",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pgl mime：.pgl=application/x-pgl
        /// </summary>
        public static MimeModel Pgl => new()
        {
            Name = "Pgl",
            Extension = ".pgl",
            ContentType = "application/x-pgl",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pic mime：.pic=application/x-pic
        /// </summary>
        public static MimeModel Pic => new()
        {
            Name = "Pic",
            Extension = ".pic",
            ContentType = "application/x-pic",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pko mime：.pko=application-pki.pko
        /// </summary>
        public static MimeModel Pko => new()
        {
            Name = "Pko",
            Extension = ".pko",
            ContentType = "application-pki.pko",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pl mime：.pl=application/x-perl
        /// </summary>
        public static MimeModel Pl => new()
        {
            Name = "Pl",
            Extension = ".pl",
            ContentType = "application/x-perl",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Plg mime：.plg=text/html
        /// </summary>
        public static MimeModel Plg => new()
        {
            Name = "Plg",
            Extension = ".plg",
            ContentType = "text/html",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pls mime：.pls=audio/scpls
        /// </summary>
        public static MimeModel Pls => new()
        {
            Name = "Pls",
            Extension = ".pls",
            ContentType = "audio/scpls",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Plt mime：.plt=application/x-plt
        /// </summary>
        public static MimeModel Plt => new()
        {
            Name = "Plt",
            Extension = ".plt",
            ContentType = "application/x-plt",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Png mime：.png=image/png，.png=application/x-png
        /// </summary>
        public static MimeModel Png => new()
        {
            Name = "Png",
            Extension = ".png",
            ContentType = "image/png",
            Alias = new List<string>
            {
                "application/x-png",
            },
        };

        /// <summary>
        /// Pot mime：.pot=applications-powerpoint
        /// </summary>
        public static MimeModel Pot => new()
        {
            Name = "Pot",
            Extension = ".pot",
            ContentType = "applications-powerpoint",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ppa mime：.ppa=application/vs-powerpoint
        /// </summary>
        public static MimeModel Ppa => new()
        {
            Name = "Ppa",
            Extension = ".ppa",
            ContentType = "application/vs-powerpoint",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ppm mime：.ppm=application/x-ppm
        /// </summary>
        public static MimeModel Ppm => new()
        {
            Name = "Ppm",
            Extension = ".ppm",
            ContentType = "application/x-ppm",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pps mime：.pps=application-powerpoint
        /// </summary>
        public static MimeModel Pps => new()
        {
            Name = "Pps",
            Extension = ".pps",
            ContentType = "application-powerpoint",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ppt mime：.ppt=applications-powerpoint，.ppt=application/x-ppt
        /// </summary>
        public static MimeModel Ppt => new()
        {
            Name = "Ppt",
            Extension = ".ppt",
            ContentType = "applications-powerpoint",
            Alias = new List<string>
            {
                "application/x-ppt",
            },
        };

        /// <summary>
        /// Pr mime：.pr=application/x-pr
        /// </summary>
        public static MimeModel Pr => new()
        {
            Name = "Pr",
            Extension = ".pr",
            ContentType = "application/x-pr",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Prf mime：.prf=application/pics-rules
        /// </summary>
        public static MimeModel Prf => new()
        {
            Name = "Prf",
            Extension = ".prf",
            ContentType = "application/pics-rules",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Prn mime：.prn=application/x-prn
        /// </summary>
        public static MimeModel Prn => new()
        {
            Name = "Prn",
            Extension = ".prn",
            ContentType = "application/x-prn",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Prt mime：.prt=application/x-prt
        /// </summary>
        public static MimeModel Prt => new()
        {
            Name = "Prt",
            Extension = ".prt",
            ContentType = "application/x-prt",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ps mime：.ps=application/x-ps，.ps=application/postscript
        /// </summary>
        public static MimeModel Ps => new()
        {
            Name = "Ps",
            Extension = ".ps",
            ContentType = "application/x-ps",
            Alias = new List<string>
            {
                "application/postscript",
            },
        };

        /// <summary>
        /// Ptn mime：.ptn=application/x-ptn
        /// </summary>
        public static MimeModel Ptn => new()
        {
            Name = "Ptn",
            Extension = ".ptn",
            ContentType = "application/x-ptn",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Pwz mime：.pwz=application/powerpoint
        /// </summary>
        public static MimeModel Pwz => new()
        {
            Name = "Pwz",
            Extension = ".pwz",
            ContentType = "application/powerpoint",
            Alias = new List<string>(),
        };

        /// <summary>
        /// R3t mime：.r3t=text/vnd.rn-realtext3d
        /// </summary>
        public static MimeModel R3T => new()
        {
            Name = "R3T",
            Extension = ".r3t",
            ContentType = "text/vnd.rn-realtext3d",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ra mime：.ra=audio/vnd.rn-realaudio
        /// </summary>
        public static MimeModel Ra => new()
        {
            Name = "Ra",
            Extension = ".ra",
            ContentType = "audio/vnd.rn-realaudio",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ram mime：.ram=audio/x-pn-realaudio
        /// </summary>
        public static MimeModel Ram => new()
        {
            Name = "Ram",
            Extension = ".ram",
            ContentType = "audio/x-pn-realaudio",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ras mime：.ras=application/x-ras
        /// </summary>
        public static MimeModel Ras => new()
        {
            Name = "Ras",
            Extension = ".ras",
            ContentType = "application/x-ras",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rat mime：.rat=application/rat-file
        /// </summary>
        public static MimeModel Rat => new()
        {
            Name = "Rat",
            Extension = ".rat",
            ContentType = "application/rat-file",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rdf mime：.rdf=text/xml
        /// </summary>
        public static MimeModel Rdf => new()
        {
            Name = "Rdf",
            Extension = ".rdf",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rec mime：.rec=application/vnd.rn-recording
        /// </summary>
        public static MimeModel Rec => new()
        {
            Name = "Rec",
            Extension = ".rec",
            ContentType = "application/vnd.rn-recording",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Red mime：.red=application/x-red
        /// </summary>
        public static MimeModel Red => new()
        {
            Name = "Red",
            Extension = ".red",
            ContentType = "application/x-red",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rgb mime：.rgb=application/x-rgb
        /// </summary>
        public static MimeModel Rgb => new()
        {
            Name = "Rgb",
            Extension = ".rgb",
            ContentType = "application/x-rgb",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rjs mime：.rjs=application/vnd.rn-realsystem-rjs
        /// </summary>
        public static MimeModel Rjs => new()
        {
            Name = "Rjs",
            Extension = ".rjs",
            ContentType = "application/vnd.rn-realsystem-rjs",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rjt mime：.rjt=application/vnd.rn-realsystem-rjt
        /// </summary>
        public static MimeModel Rjt => new()
        {
            Name = "Rjt",
            Extension = ".rjt",
            ContentType = "application/vnd.rn-realsystem-rjt",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rlc mime：.rlc=application/x-rlc
        /// </summary>
        public static MimeModel Rlc => new()
        {
            Name = "Rlc",
            Extension = ".rlc",
            ContentType = "application/x-rlc",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rle mime：.rle=application/x-rle
        /// </summary>
        public static MimeModel Rle => new()
        {
            Name = "Rle",
            Extension = ".rle",
            ContentType = "application/x-rle",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rm mime：.rm=application/vnd.rn-realmedia
        /// </summary>
        public static MimeModel Rm => new()
        {
            Name = "Rm",
            Extension = ".rm",
            ContentType = "application/vnd.rn-realmedia",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rmf mime：.rmf=application/vnd.adobe.rmf
        /// </summary>
        public static MimeModel Rmf => new()
        {
            Name = "Rmf",
            Extension = ".rmf",
            ContentType = "application/vnd.adobe.rmf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rmi mime：.rmi=audio/mid
        /// </summary>
        public static MimeModel Rmi => new()
        {
            Name = "Rmi",
            Extension = ".rmi",
            ContentType = "audio/mid",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rmj mime：.rmj=application/vnd.rn-realsystem-rmj
        /// </summary>
        public static MimeModel Rmj => new()
        {
            Name = "Rmj",
            Extension = ".rmj",
            ContentType = "application/vnd.rn-realsystem-rmj",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rmm mime：.rmm=audio/x-pn-realaudio
        /// </summary>
        public static MimeModel Rmm => new()
        {
            Name = "Rmm",
            Extension = ".rmm",
            ContentType = "audio/x-pn-realaudio",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rmp mime：.rmp=application/vnd.rn-rn_music_package
        /// </summary>
        public static MimeModel Rmp => new()
        {
            Name = "Rmp",
            Extension = ".rmp",
            ContentType = "application/vnd.rn-rn_music_package",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rms mime：.rms=application/vnd.rn-realmedia-secure
        /// </summary>
        public static MimeModel Rms => new()
        {
            Name = "Rms",
            Extension = ".rms",
            ContentType = "application/vnd.rn-realmedia-secure",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rmvb mime：.rmvb=application/vnd.rn-realmedia-vbr
        /// </summary>
        public static MimeModel Rmvb => new()
        {
            Name = "Rmvb",
            Extension = ".rmvb",
            ContentType = "application/vnd.rn-realmedia-vbr",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rmx mime：.rmx=application/vnd.rn-realsystem-rmx
        /// </summary>
        public static MimeModel Rmx => new()
        {
            Name = "Rmx",
            Extension = ".rmx",
            ContentType = "application/vnd.rn-realsystem-rmx",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rnx mime：.rnx=application/vnd.rn-realplayer
        /// </summary>
        public static MimeModel Rnx => new()
        {
            Name = "Rnx",
            Extension = ".rnx",
            ContentType = "application/vnd.rn-realplayer",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rp mime：.rp=image/vnd.rn-realpix
        /// </summary>
        public static MimeModel Rp => new()
        {
            Name = "Rp",
            Extension = ".rp",
            ContentType = "image/vnd.rn-realpix",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rpm mime：.rpm=audio/x-pn-realaudio-plugin
        /// </summary>
        public static MimeModel Rpm => new()
        {
            Name = "Rpm",
            Extension = ".rpm",
            ContentType = "audio/x-pn-realaudio-plugin",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rsml mime：.rsml=application/vnd.rn-rsml
        /// </summary>
        public static MimeModel Rsml => new()
        {
            Name = "Rsml",
            Extension = ".rsml",
            ContentType = "application/vnd.rn-rsml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rt mime：.rt=text/vnd.rn-realtext
        /// </summary>
        public static MimeModel Rt => new()
        {
            Name = "Rt",
            Extension = ".rt",
            ContentType = "text/vnd.rn-realtext",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Rtf mime：.rtf=application/msword，.rtf=application/x-rtf
        /// </summary>
        public static MimeModel Rtf => new()
        {
            Name = "Rtf",
            Extension = ".rtf",
            ContentType = "application/msword",
            Alias = new List<string>
            {
                "application/x-rtf",
            },
        };

        /// <summary>
        /// Rv mime：.rv=video/vnd.rn-realvideo
        /// </summary>
        public static MimeModel Rv => new()
        {
            Name = "Rv",
            Extension = ".rv",
            ContentType = "video/vnd.rn-realvideo",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Sam mime：.sam=application/x-sam
        /// </summary>
        public static MimeModel Sam => new()
        {
            Name = "Sam",
            Extension = ".sam",
            ContentType = "application/x-sam",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Sat mime：.sat=application/x-sat
        /// </summary>
        public static MimeModel Sat => new()
        {
            Name = "Sat",
            Extension = ".sat",
            ContentType = "application/x-sat",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Sdp mime：.sdp=application/sdp
        /// </summary>
        public static MimeModel Sdp => new()
        {
            Name = "Sdp",
            Extension = ".sdp",
            ContentType = "application/sdp",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Sdw mime：.sdw=application/x-sdw
        /// </summary>
        public static MimeModel Sdw => new()
        {
            Name = "Sdw",
            Extension = ".sdw",
            ContentType = "application/x-sdw",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Sit mime：.sit=application/x-stuffit
        /// </summary>
        public static MimeModel Sit => new()
        {
            Name = "Sit",
            Extension = ".sit",
            ContentType = "application/x-stuffit",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Slb mime：.slb=application/x-slb
        /// </summary>
        public static MimeModel Slb => new()
        {
            Name = "Slb",
            Extension = ".slb",
            ContentType = "application/x-slb",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Sld mime：.sld=application/x-sld
        /// </summary>
        public static MimeModel Sld => new()
        {
            Name = "Sld",
            Extension = ".sld",
            ContentType = "application/x-sld",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Slk mime：.slk=drawing/x-slk
        /// </summary>
        public static MimeModel Slk => new()
        {
            Name = "Slk",
            Extension = ".slk",
            ContentType = "drawing/x-slk",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Smi mime：.smi=application/smil
        /// </summary>
        public static MimeModel Smi => new()
        {
            Name = "Smi",
            Extension = ".smi",
            ContentType = "application/smil",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Smil mime：.smil=application/smil
        /// </summary>
        public static MimeModel Smil => new()
        {
            Name = "Smil",
            Extension = ".smil",
            ContentType = "application/smil",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Smk mime：.smk=application/x-smk
        /// </summary>
        public static MimeModel Smk => new()
        {
            Name = "Smk",
            Extension = ".smk",
            ContentType = "application/x-smk",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Snd mime：.snd=audio/basic
        /// </summary>
        public static MimeModel Snd => new()
        {
            Name = "Snd",
            Extension = ".snd",
            ContentType = "audio/basic",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Sol mime：.sol=text/plain
        /// </summary>
        public static MimeModel Sol => new()
        {
            Name = "Sol",
            Extension = ".sol",
            ContentType = "text/plain",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Sor mime：.sor=text/plain
        /// </summary>
        public static MimeModel Sor => new()
        {
            Name = "Sor",
            Extension = ".sor",
            ContentType = "text/plain",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Spc mime：.spc=application/x-pkcs7-certificates
        /// </summary>
        public static MimeModel Spc => new()
        {
            Name = "Spc",
            Extension = ".spc",
            ContentType = "application/x-pkcs7-certificates",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Spl mime：.spl=application/futuresplash
        /// </summary>
        public static MimeModel Spl => new()
        {
            Name = "Spl",
            Extension = ".spl",
            ContentType = "application/futuresplash",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Spp mime：.spp=text/xml
        /// </summary>
        public static MimeModel Spp => new()
        {
            Name = "Spp",
            Extension = ".spp",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ssm mime：.ssm=application/streamingmedia
        /// </summary>
        public static MimeModel Ssm => new()
        {
            Name = "Ssm",
            Extension = ".ssm",
            ContentType = "application/streamingmedia",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Sst mime：.sst=application-pki.certstore
        /// </summary>
        public static MimeModel Sst => new()
        {
            Name = "Sst",
            Extension = ".sst",
            ContentType = "application-pki.certstore",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Stl mime：.stl=application/-pki.stl
        /// </summary>
        public static MimeModel Stl => new()
        {
            Name = "Stl",
            Extension = ".stl",
            ContentType = "application/-pki.stl",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Stm mime：.stm=text/html
        /// </summary>
        public static MimeModel Stm => new()
        {
            Name = "Stm",
            Extension = ".stm",
            ContentType = "text/html",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Sty mime：.sty=application/x-sty
        /// </summary>
        public static MimeModel Sty => new()
        {
            Name = "Sty",
            Extension = ".sty",
            ContentType = "application/x-sty",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Svg mime：.svg=text/xml
        /// </summary>
        public static MimeModel Svg => new()
        {
            Name = "Svg",
            Extension = ".svg",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Swf mime：.swf=application/x-shockwave-flash
        /// </summary>
        public static MimeModel Swf => new()
        {
            Name = "Swf",
            Extension = ".swf",
            ContentType = "application/x-shockwave-flash",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Tdf mime：.tdf=application/x-tdf
        /// </summary>
        public static MimeModel Tdf => new()
        {
            Name = "Tdf",
            Extension = ".tdf",
            ContentType = "application/x-tdf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Tg4 mime：.tg4=application/x-tg4
        /// </summary>
        public static MimeModel Tg4 => new()
        {
            Name = "Tg4",
            Extension = ".tg4",
            ContentType = "application/x-tg4",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Tga mime：.tga=application/x-tga
        /// </summary>
        public static MimeModel Tga => new()
        {
            Name = "Tga",
            Extension = ".tga",
            ContentType = "application/x-tga",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Tif mime：.tif=image/tiff，.tif=application/x-tif
        /// </summary>
        public static MimeModel Tif => new()
        {
            Name = "Tif",
            Extension = ".tif",
            ContentType = "image/tiff",
            Alias = new List<string>
            {
                "application/x-tif",
            },
        };

        /// <summary>
        /// Tiff mime：.tiff=image/tiff
        /// </summary>
        public static MimeModel Tiff => new()
        {
            Name = "Tiff",
            Extension = ".tiff",
            ContentType = "image/tiff",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Tld mime：.tld=text/xml
        /// </summary>
        public static MimeModel Tld => new()
        {
            Name = "Tld",
            Extension = ".tld",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Top mime：.top=drawing/x-top
        /// </summary>
        public static MimeModel Top => new()
        {
            Name = "Top",
            Extension = ".top",
            ContentType = "drawing/x-top",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Torrent mime：.torrent=application/x-bittorrent
        /// </summary>
        public static MimeModel Torrent => new()
        {
            Name = "Torrent",
            Extension = ".torrent",
            ContentType = "application/x-bittorrent",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Tsd mime：.tsd=text/xml
        /// </summary>
        public static MimeModel Tsd => new()
        {
            Name = "Tsd",
            Extension = ".tsd",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Txt mime：.txt=text/plain
        /// </summary>
        public static MimeModel Txt => new()
        {
            Name = "Txt",
            Extension = ".txt",
            ContentType = "text/plain",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Uin mime：.uin=application/x-icq
        /// </summary>
        public static MimeModel Uin => new()
        {
            Name = "Uin",
            Extension = ".uin",
            ContentType = "application/x-icq",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Uls mime：.uls=text/iuls
        /// </summary>
        public static MimeModel Uls => new()
        {
            Name = "Uls",
            Extension = ".uls",
            ContentType = "text/iuls",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Vcf mime：.vcf=text/x-vcard
        /// </summary>
        public static MimeModel Vcf => new()
        {
            Name = "Vcf",
            Extension = ".vcf",
            ContentType = "text/x-vcard",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Vda mime：.vda=application/x-vda
        /// </summary>
        public static MimeModel Vda => new()
        {
            Name = "Vda",
            Extension = ".vda",
            ContentType = "application/x-vda",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Vdx mime：.vdx=application/vnd.visio
        /// </summary>
        public static MimeModel Vdx => new()
        {
            Name = "Vdx",
            Extension = ".vdx",
            ContentType = "application/vnd.visio",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Vml mime：.vml=text/xml
        /// </summary>
        public static MimeModel Vml => new()
        {
            Name = "Vml",
            Extension = ".vml",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Vpg mime：.vpg=application/x-vpeg005
        /// </summary>
        public static MimeModel Vpg => new()
        {
            Name = "Vpg",
            Extension = ".vpg",
            ContentType = "application/x-vpeg005",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Vsd mime：.vsd=application/vnd.visio， .vsd=application/x-tif
        /// </summary>
        public static MimeModel Vsd => new()
        {
            Name = "Vsd",
            Extension = ".vsd",
            ContentType = "application/vnd.visio",
            Alias = new List<string>
            {
                "application/x-vsd",
            },
        };

        /// <summary>
        /// Vss mime：.vss=application/vnd.visio
        /// </summary>
        public static MimeModel Vss => new()
        {
            Name = "Vss",
            Extension = ".vss",
            ContentType = "application/vnd.visio",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Vst mime：.vst=application/vnd.visio，.vst=application/x-vst
        /// </summary>
        public static MimeModel Vst => new()
        {
            Name = "Vst",
            Extension = ".vst",
            ContentType = "application/vnd.visio",
            Alias = new List<string>
            {
                "application/x-vst",
            },
        };

        /// <summary>
        /// Vsw mime：.vsw=application/vnd.visio
        /// </summary>
        public static MimeModel Vsw => new()
        {
            Name = "Vsw",
            Extension = ".vsw",
            ContentType = "application/vnd.visio",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Vsx mime：.vsx=application/vnd.visio
        /// </summary>
        public static MimeModel Vsx => new()
        {
            Name = "Vsx",
            Extension = ".vsx",
            ContentType = "application/vnd.visio",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Vtx mime：.vtx=application/vnd.visio
        /// </summary>
        public static MimeModel Vtx => new()
        {
            Name = "Vtx",
            Extension = ".vtx",
            ContentType = "application/vnd.visio",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Vxml mime：.vxml=text/xml
        /// </summary>
        public static MimeModel Vxml => new()
        {
            Name = "Vxml",
            Extension = ".vxml",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wav mime：.wav=audio/wav
        /// </summary>
        public static MimeModel Wav => new()
        {
            Name = "Wav",
            Extension = ".wav",
            ContentType = "audio/wav",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wax mime：.wax=audio/x-ms-wax
        /// </summary>
        public static MimeModel Wax => new()
        {
            Name = "Wax",
            Extension = ".wax",
            ContentType = "audio/x-ms-wax",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wb1 mime：.wb1=application/x-wb1
        /// </summary>
        public static MimeModel Wb1 => new()
        {
            Name = "Wb1",
            Extension = ".wb1",
            ContentType = "application/x-wb1",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wb2 mime：.wb2=application/x-wb2
        /// </summary>
        public static MimeModel Wb2 => new()
        {
            Name = "Wb2",
            Extension = ".wb2",
            ContentType = "application/x-wb2",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wb3 mime：.wb3=application/x-wb3
        /// </summary>
        public static MimeModel Wb3 => new()
        {
            Name = "Wb3",
            Extension = ".wb3",
            ContentType = "application/x-wb3",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wbmp mime：.wbmp=image/vnd.wap.wbmp
        /// </summary>
        public static MimeModel Wbmp => new()
        {
            Name = "Wbmp",
            Extension = ".wbmp",
            ContentType = "image/vnd.wap.wbmp",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wiz mime：.wiz=application/msword
        /// </summary>
        public static MimeModel Wiz => new()
        {
            Name = "Wiz",
            Extension = ".wiz",
            ContentType = "application/msword",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wk3 mime：.wk3=application/x-wk3
        /// </summary>
        public static MimeModel Wk3 => new()
        {
            Name = "Wk3",
            Extension = ".wk3",
            ContentType = "application/x-wk3",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wk4 mime：.wk4=application/x-wk4
        /// </summary>
        public static MimeModel Wk4 => new()
        {
            Name = "Wk4",
            Extension = ".wk4",
            ContentType = "application/x-wk4",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wkq mime：.wkq=application/x-wkq
        /// </summary>
        public static MimeModel Wkq => new()
        {
            Name = "Wkq",
            Extension = ".wkq",
            ContentType = "application/x-wkq",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wks mime：.wks=application/x-wks
        /// </summary>
        public static MimeModel Wks => new()
        {
            Name = "Wks",
            Extension = ".wks",
            ContentType = "application/x-wks",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wm mime：.wm=video/x-ms-wm
        /// </summary>
        public static MimeModel Wm => new()
        {
            Name = "Wm",
            Extension = ".wm",
            ContentType = "video/x-ms-wm",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wma mime：.wma=audio/x-ms-wma
        /// </summary>
        public static MimeModel Wma => new()
        {
            Name = "Wma",
            Extension = ".wma",
            ContentType = "audio/x-ms-wma",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wmd mime：.wmd=application/x-ms-wmd
        /// </summary>
        public static MimeModel Wmd => new()
        {
            Name = "Wmd",
            Extension = ".wmd",
            ContentType = "application/x-ms-wmd",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wmf mime：.wmf=application/x-wmf
        /// </summary>
        public static MimeModel Wmf => new()
        {
            Name = "Wmf",
            Extension = ".wmf",
            ContentType = "application/x-wmf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wml mime：.wml=text/vnd.wap.wml
        /// </summary>
        public static MimeModel Wml => new()
        {
            Name = "Wml",
            Extension = ".wml",
            ContentType = "text/vnd.wap.wml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wmv mime：.wmv=video/x-ms-wmv
        /// </summary>
        public static MimeModel Wmv => new()
        {
            Name = "Wmv",
            Extension = ".wmv",
            ContentType = "video/x-ms-wmv",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wmx mime：.wmx=video/x-ms-wmx
        /// </summary>
        public static MimeModel Wmx => new()
        {
            Name = "Wmx",
            Extension = ".wmx",
            ContentType = "video/x-ms-wmx",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wmz mime：.wmz=application/x-ms-wmz
        /// </summary>
        public static MimeModel Wmz => new()
        {
            Name = "Wmz",
            Extension = ".wmz",
            ContentType = "application/x-ms-wmz",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wp6 mime：.wp6=application/x-wp6
        /// </summary>
        public static MimeModel Wp6 => new()
        {
            Name = "Wp6",
            Extension = ".wp6",
            ContentType = "application/x-wp6",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wpd mime：.wpd=application/x-wpd
        /// </summary>
        public static MimeModel Wpd => new()
        {
            Name = "Wpd",
            Extension = ".wpd",
            ContentType = "application/x-wpd",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wpg mime：.wpg=application/x-wpg
        /// </summary>
        public static MimeModel Wpg => new()
        {
            Name = "Wpg",
            Extension = ".wpg",
            ContentType = "application/x-wpg",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wpl mime：.wpl=application/-wpl
        /// </summary>
        public static MimeModel Wpl => new()
        {
            Name = "Wpl",
            Extension = ".wpl",
            ContentType = "application/-wpl",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wq1 mime：.wq1=application/x-wq1
        /// </summary>
        public static MimeModel Wq1 => new()
        {
            Name = "Wq1",
            Extension = ".wq1",
            ContentType = "application/x-wq1",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wr1 mime：.wr1=application/x-wr1
        /// </summary>
        public static MimeModel Wr1 => new()
        {
            Name = "Wr1",
            Extension = ".wr1",
            ContentType = "application/x-wr1",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wri mime：.wri=application/x-wri
        /// </summary>
        public static MimeModel Wri => new()
        {
            Name = "Wri",
            Extension = ".wri",
            ContentType = "application/x-wri",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wrk mime：.wrk=application/x-wrk
        /// </summary>
        public static MimeModel Wrk => new()
        {
            Name = "Wrk",
            Extension = ".wrk",
            ContentType = "application/x-wrk",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ws mime：.ws=application/x-ws
        /// </summary>
        public static MimeModel Ws => new()
        {
            Name = "Ws",
            Extension = ".ws",
            ContentType = "application/x-ws",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Ws2 mime：.ws2=application/x-ws
        /// </summary>
        public static MimeModel Ws2 => new()
        {
            Name = "Ws2",
            Extension = ".ws2",
            ContentType = "application/x-ws",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wsc mime：.wsc=text/scriptlet
        /// </summary>
        public static MimeModel Wsc => new()
        {
            Name = "Wsc",
            Extension = ".wsc",
            ContentType = "text/scriptlet",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wsdl mime：.wsdl=text/xml
        /// </summary>
        public static MimeModel Wsdl => new()
        {
            Name = "Wsdl",
            Extension = ".wsdl",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Wvx mime：.wvx=video/x-ms-wvx
        /// </summary>
        public static MimeModel Wvx => new()
        {
            Name = "Wvx",
            Extension = ".wvx",
            ContentType = "video/x-ms-wvx",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xdp mime：.xdp=application/vnd.adobe.xdp
        /// </summary>
        public static MimeModel Xdp => new()
        {
            Name = "Xdp",
            Extension = ".xdp",
            ContentType = "application/vnd.adobe.xdp",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xdr mime：.xdr=text/xml
        /// </summary>
        public static MimeModel Xdr => new()
        {
            Name = "Xdr",
            Extension = ".xdr",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xfd mime：.xfd=application/vnd.adobe.xfd
        /// </summary>
        public static MimeModel Xfd => new()
        {
            Name = "Xfd",
            Extension = ".xfd",
            ContentType = "application/vnd.adobe.xfd",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xfdf mime：.xfdf=application/vnd.adobe.xfdf
        /// </summary>
        public static MimeModel Xfdf => new()
        {
            Name = "Xfdf",
            Extension = ".xfdf",
            ContentType = "application/vnd.adobe.xfdf",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xhtml mime：.xhtml=text/html
        /// </summary>
        public static MimeModel Xhtml => new()
        {
            Name = "Xhtml",
            Extension = ".xhtml",
            ContentType = "text/html",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xls mime：.xls=application/-excel，.xls=application/x-xls
        /// </summary>
        public static MimeModel Xls => new()
        {
            Name = "Xls",
            Extension = ".xls",
            ContentType = "application/-excel",
            Alias = new List<string>
            {
                "application/x-xls",
            },
        };

        /// <summary>
        /// Xlw mime：.xlw=application/x-xlw
        /// </summary>
        public static MimeModel Xlw => new()
        {
            Name = "Xlw",
            Extension = ".xlw",
            ContentType = "application/x-xlw",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xml mime：.xml=text/xml
        /// </summary>
        public static MimeModel Xml => new()
        {
            Name = "Xml",
            Extension = ".xml",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xpl mime：.xpl=audio/scpls
        /// </summary>
        public static MimeModel Xpl => new()
        {
            Name = "Xpl",
            Extension = ".xpl",
            ContentType = "audio/scpls",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xq mime：.xq=text/xml
        /// </summary>
        public static MimeModel Xq => new()
        {
            Name = "Xq",
            Extension = ".xq",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xql mime：.xql=text/xml
        /// </summary>
        public static MimeModel Xql => new()
        {
            Name = "Xql",
            Extension = ".xql",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xquery mime：.xquery=text/xml
        /// </summary>
        public static MimeModel Xquery => new()
        {
            Name = "Xquery",
            Extension = ".xquery",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xsd mime：.xsd=text/xml
        /// </summary>
        public static MimeModel Xsd => new()
        {
            Name = "Xsd",
            Extension = ".xsd",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xsl mime：.xsl=text/xml
        /// </summary>
        public static MimeModel Xsl => new()
        {
            Name = "Xsl",
            Extension = ".xsl",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xslt mime：.xslt=text/xml
        /// </summary>
        public static MimeModel Xslt => new()
        {
            Name = "Xslt",
            Extension = ".xslt",
            ContentType = "text/xml",
            Alias = new List<string>(),
        };

        /// <summary>
        /// Xwd mime：.xwd=application/x-xwd
        /// </summary>
        public static MimeModel Xwd => new()
        {
            Name = "Xwd",
            Extension = ".xwd",
            ContentType = "application/x-xwd",
            Alias = new List<string>(),
        };

        /// <summary>
        /// X_b mime：.x_b=application/x-x_b
        /// </summary>
        public static MimeModel Xb => new()
        {
            Name = "Xb",
            Extension = ".x_b",
            ContentType = "application/x-x_b",
            Alias = new List<string>(),
        };

        /// <summary>
        /// X_t mime：.x_t=application/x-x_t
        /// </summary>
        public static MimeModel Xt => new()
        {
            Name = "Xt",
            Extension = ".x_t",
            ContentType = "application/x-x_t",
            Alias = new List<string>(),
        };

        /// <summary>
        /// dotx mime：.x_b=application/vnd.openxmlformats-officedocument.wordprocessingml.template
        /// </summary>
        public static MimeModel Dotx => new()
        {
            Name = "Dotx",
            Extension = ".dotx",
            ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.template",
            Alias = new List<string>(),
        };

        /// <summary>
        /// dotx mime：.x_b=application/vnd.openxmlformats-officedocument.presentationml.presentation
        /// </summary>
        public static MimeModel Pptx => new()
        {
            Name = "Pptx",
            Extension = ".pptx",
            ContentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            Alias = new List<string>(),
        };

        /// <summary>
        /// ppsx mime：.x_b=application/vnd.openxmlformats-officedocument.presentationml.slideshow
        /// </summary>
        public static MimeModel Ppsx => new()
        {
            Name = "Ppsx",
            Extension = ".ppsx",
            ContentType = "application/vnd.openxmlformats-officedocument.presentationml.slideshow",
            Alias = new List<string>(),
        };

        /// <summary>
        /// potx mime：.x_b=application/vnd.openxmlformats-officedocument.presentationml.template
        /// </summary>
        public static MimeModel Potx => new()
        {
            Name = "Potx",
            Extension = ".potx",
            ContentType = "application/vnd.openxmlformats-officedocument.presentationml.template",
            Alias = new List<string>(),
        };

        /// <summary>
        /// xlsx mime：.x_b=application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
        /// </summary>
        public static MimeModel Xlsx => new()
        {
            Name = "Xlsx",
            Extension = ".xlsx",
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            Alias = new List<string>(),
        };

        /// <summary>
        /// xltx mime：.x_b=application/vnd.openxmlformats-officedocument.spreadsheetml.template
        /// </summary>
        public static MimeModel Xltx => new()
        {
            Name = "Xltx ",
            Extension = ".xltx",
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.template",
            Alias = new List<string>(),
        };

        /// <summary>
        /// MsExcel mime：.x_b=application/vnd.ms-excel
        /// </summary>
        public static MimeModel MsExcel => new()
        {
            Name = "MsExcel",
            Extension = ".xls",
            ContentType = "application/vnd.ms-excel",
            Alias = new List<string>(),
        };
    }
}