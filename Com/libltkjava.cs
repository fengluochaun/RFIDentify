using com.sun.org.apache.xpath.@internal;
using CsvHelper;
using java.io;
using java.lang;
using java.nio.charset;
using java.util;
using jdk.nashorn.@internal.ir;
using org.apache.log4j;
using org.jdom;
using org.jdom.input;
using org.jdom.output;
using org.llrp.ltk.exceptions;
using org.llrp.ltk.generated;
using org.llrp.ltk.generated.custom.messages;
using org.llrp.ltk.generated.custom.parameters;
using org.llrp.ltk.generated.enumerations;
using org.llrp.ltk.generated.messages;
using org.llrp.ltk.generated.parameters;
using org.llrp.ltk.net;
using org.llrp.ltk.types;
using org.llrp.ltk.util;
using sun.swing;
using System.Globalization;
using System.Numerics;

namespace RFIDentify.Com
{
    public class libltkjava : LLRPEndpoint
    {
        private bool begin = false;
        private long beginTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
		#region 变量
		private LLRPConnection connection;
        public static string? WriteCsvFilePath { get; set; }
		private readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;

		static Logger logger = Logger.getLogger("org.impinj.llrp.ltk.examples.docsample4");
        private ROSpec rospec;
        private int MessageID = 23; // a random starting point
        private UnsignedInteger modelName;
        UnsignedShort maxPowerIndex;
        SignedShort maxPower;
        UnsignedShort channelIndex;
        UnsignedShort hopTableID;

        // stuff to calculate velocity
        int phaseCount;
        string? lastEPCData;
        UnsignedShort lastAntennaID;
        UnsignedShort lastChannelIndex;
        UnsignedLong lastReadTime;
        string? currentEPCData;
        private UnsignedShort currentAntennaID;
        UnsignedShort currentChannelIndex;
        UnsignedLong currentReadTime;
        double lastRfPhase;
        public double currentRfPhase;
        public double currentPeakRSSI;
        public string epcname;
        #endregion

        private UnsignedInteger getUniqueMessageID()
        {
            return new UnsignedInteger(MessageID++);
        }

        public libltkjava()
        {
            lastEPCData = null;
            lastAntennaID = new UnsignedShort(0);
            lastChannelIndex = new org.llrp.ltk.types.UnsignedShort(0);
            Long l = new Long(0);
            lastReadTime = new UnsignedLong(l);
            lastRfPhase = 0;

        }

        private void connect(string ip)
        {
            // create client-initiated LLRP connection

            connection = new LLRPConnector(this, ip);

            // connect to reader
            // LLRPConnector.connect waits for successful
            // READER_EVENT_NOTIFICATION from reader
            try
            {
                logger.info("Initiate LLRP connection to reader");
                ((LLRPConnector)connection).connect();
            }
            catch (LLRPConnectionAttemptFailedException e1)
            {
                e1.printStackTrace();
                Application.Exit();
            }
        }

        private void disconnect()
        {
            LLRPMessage response;
            CLOSE_CONNECTION close = new CLOSE_CONNECTION();
            close.setMessageID(getUniqueMessageID());
            try
            {
                // don't wait around too long for close
                response = connection.transact(close, 4000);

                // check whether ROSpec addition was successful
                StatusCode status = ((CLOSE_CONNECTION_RESPONSE)response).getLLRPStatus().getStatusCode();
                if (status.equals(new StatusCode("M_Success")))
                {
                    logger.info("CLOSE_CONNECTION was successful");
                }
                else
                {
                    logger.info(response.toXMLString());
                    logger.info("CLOSE_CONNECTION Failed ... continuing anyway");
                }

            }
            catch (InvalidLLRPMessageException)
            {
                logger.error("CLOSE_CONNECTION: Received invalid response message");
            }
            catch (TimeoutException)
            {
                logger.info("CLOSE_CONNECTION Timeouts ... continuing anyway");
            }
        }

        private void enableImpinjExtensions()
        {
            LLRPMessage response;

            try
            {
                logger.info("IMPINJ_ENABLE_EXTENSIONS ...");
                IMPINJ_ENABLE_EXTENSIONS ena = new IMPINJ_ENABLE_EXTENSIONS();
                ena.setMessageID(getUniqueMessageID());

                response = connection.transact(ena, 10000);

                StatusCode status = ((IMPINJ_ENABLE_EXTENSIONS_RESPONSE)response).getLLRPStatus().getStatusCode();
                if (status.equals(new StatusCode("M_Success")))
                {
                    logger.info("IMPINJ_ENABLE_EXTENSIONS was successful");
                }
                else
                {
                    logger.info(response.toXMLString());
                    logger.info("IMPINJ_ENABLE_EXTENSIONS Failure");
                    Application.Exit();
                }
            }
            catch (InvalidLLRPMessageException)
            {
                logger.error("Could not process IMPINJ_ENABLE_EXTENSIONS response");
                Application.Exit();
            }
            catch (TimeoutException)
            {
                logger.error("Timeout Waiting for IMPINJ_ENABLE_EXTENSIONS response");
                Application.Exit();
            }
        }

        private void factoryDefault()
        {
            LLRPMessage response;

            try
            {
                // factory default the reader
                logger.info("SET_READER_CONFIG with factory default ...");
                SET_READER_CONFIG set = new SET_READER_CONFIG();
                set.setMessageID(getUniqueMessageID());
                set.setResetToFactoryDefault(new Bit(true));
                response = connection.transact(set, 10000);

                // check whether ROSpec addition was successful
                StatusCode status = ((SET_READER_CONFIG_RESPONSE)response).getLLRPStatus().getStatusCode();
                if (status.equals(new StatusCode("M_Success")))
                {
                    logger.info("SET_READER_CONFIG Factory Default was successful");
                }
                else
                {
                    logger.info(response.toXMLString());
                    logger.info("SET_READER_CONFIG Factory Default Failure");
                    Application.Exit();
                }

            }
            catch (java.lang.Exception e)
            {
                e.printStackTrace();
                Application.Exit();
            }
        }

        private void getReaderCapabilities()
        {
            LLRPMessage response;
            GET_READER_CAPABILITIES_RESPONSE gresp;

            GET_READER_CAPABILITIES get = new GET_READER_CAPABILITIES();
            GetReaderCapabilitiesRequestedData data =
                    new GetReaderCapabilitiesRequestedData(
                            GetReaderCapabilitiesRequestedData.All);
            get.setRequestedData(data);
            get.setMessageID(getUniqueMessageID());
            logger.info("Sending GET_READER_CAPABILITIES message  ...");
            try
            {
                response = connection.transact(get, 10000);

                // check whether GET_CAPABILITIES addition was successful
                gresp = (GET_READER_CAPABILITIES_RESPONSE)response;
                StatusCode status = gresp.getLLRPStatus().getStatusCode();
                if (status.equals(new StatusCode("M_Success")))
                {
                    logger.info("GET_READER_CAPABILITIES was successful");

                    // get the info we need
                    GeneralDeviceCapabilities dev_cap = gresp.getGeneralDeviceCapabilities();
                    if ((dev_cap == null) ||
                        (!dev_cap.getDeviceManufacturerName().equals(new UnsignedInteger(25882))))
                    {
                        logger.error("DocSample4 must use Impinj model Reader, not " +
                            dev_cap.getDeviceManufacturerName().toString());
                        Application.Exit();
                    }

                    modelName = dev_cap.getModelName();
                    logger.info("Found Impinj reader model " + modelName.toString());

                    // get the max power level
                    if (gresp.getRegulatoryCapabilities() != null)
                    {
                        UHFBandCapabilities band_cap =
                            gresp.getRegulatoryCapabilities().getUHFBandCapabilities();

                        java.util.List pwr_list =
                            band_cap.getTransmitPowerLevelTableEntryList();

                        TransmitPowerLevelTableEntry entry =
                            (TransmitPowerLevelTableEntry)pwr_list.get(pwr_list.size() - 1);

                        maxPowerIndex = entry.getIndex();
                        maxPower = entry.getTransmitPowerValue();
                        // LLRP sends power in dBm * 100
                        double d = ((double)maxPower.intValue()) / 100;

                        logger.info("Max power " + d +
                                    " dBm at index " + maxPowerIndex.toString());
                    }
                }
                else
                {
                    logger.info(response.toXMLString());
                    logger.info("GET_READER_CAPABILITIES failures");
                    Application.Exit();
                }
            }
            catch (InvalidLLRPMessageException)
            {
                logger.error("Could not display response string");
            }
            catch (TimeoutException)
            {
                logger.error("Timeout waiting for GET_READER_CAPABILITIES response");
                Application.Exit();
            }
        }

        private void getReaderConfiguration()
        {
            LLRPMessage response;
            GET_READER_CONFIG_RESPONSE gresp;

            GET_READER_CONFIG get = new GET_READER_CONFIG();
            GetReaderConfigRequestedData data =
                    new GetReaderConfigRequestedData(
                            GetReaderConfigRequestedData.All);
            get.setRequestedData(data);
            get.setMessageID(getUniqueMessageID());
            get.setAntennaID(new UnsignedShort(0));
            get.setGPIPortNum(new UnsignedShort(0));
            get.setGPOPortNum(new UnsignedShort(0));

            logger.info("Sending GET_READER_CONFIG message  ...");
            try
            {
                response = connection.transact(get, 10000);

                // check whether GET_CAPABILITIES addition was successful
                gresp = (GET_READER_CONFIG_RESPONSE)response;
                StatusCode status = gresp.getLLRPStatus().getStatusCode();
                if (status.equals(new StatusCode("M_Success")))
                {
                    logger.info("GET_READER_CONFIG was successful");

                    java.util.List alist = gresp.getAntennaConfigurationList();

                    if (alist.size() > 0)
                    {
                        AntennaConfiguration a_cfg = (AntennaConfiguration)alist.get(0);
                        channelIndex = a_cfg.getRFTransmitter().getChannelIndex();
                        hopTableID = a_cfg.getRFTransmitter().getHopTableID();
                        //                    UnsignedShort p =  a_cfg.getRFTransmitter().getTransmitPower();
                        logger.info("ChannelIndex " + channelIndex.toString() +
                                    " hopTableID " + hopTableID.toString());
                    }
                    else
                    {
                        logger.error("Could not find antenna configuration");
                        Application.Exit();
                    }
                }
                else
                {
                    logger.info(response.toXMLString());
                    logger.info("GET_READER_CONFIG failures");
                    Application.Exit();
                }
            }
            catch (InvalidLLRPMessageException)
            {
                logger.error("Could not display response string");
            }
            catch (TimeoutException)
            {
                logger.error("Timeout waiting for GET_READER_CONFIG response");
                Application.Exit();
            }
        }

        private ADD_ROSPEC buildROSpecFromObjects()
        {
            logger.info("Building ADD_ROSPEC message from scratch ...");
            ADD_ROSPEC addRoSpec = new ADD_ROSPEC();
            addRoSpec.setMessageID(getUniqueMessageID());

            rospec = new ROSpec();

            // set up the basic info for the RO Spec.
            rospec.setCurrentState(new ROSpecState(ROSpecState.Disabled));
            rospec.setPriority(new UnsignedByte(0));
            rospec.setROSpecID(new UnsignedInteger(12345));

            // set the start and stop conditions for the ROSpec.
            // For now, we will start and stop manually 
            ROBoundarySpec boundary = new ROBoundarySpec();
            ROSpecStartTrigger start = new ROSpecStartTrigger();
            ROSpecStopTrigger stop = new ROSpecStopTrigger();
            start.setROSpecStartTriggerType(new ROSpecStartTriggerType(ROSpecStartTriggerType.Null));
            stop.setROSpecStopTriggerType(new ROSpecStopTriggerType(ROSpecStopTriggerType.Null));
            stop.setDurationTriggerValue(new UnsignedInteger(0));
            boundary.setROSpecStartTrigger(start);
            boundary.setROSpecStopTrigger(stop);
            rospec.setROBoundarySpec(boundary);

            // set up what we want to do in the ROSpec. In this case
            // build the simples inventory on all channels using defaults
            AISpec aispec = new AISpec();

            // what antennas to use.
            UnsignedShortArray ants = new UnsignedShortArray();
            ants.add(new UnsignedShort(0)); // 0 means all antennas
            aispec.setAntennaIDs(ants);

            // set up the AISpec stop condition and options for inventory
            AISpecStopTrigger aistop = new AISpecStopTrigger();
            aistop.setAISpecStopTriggerType(new AISpecStopTriggerType(AISpecStopTriggerType.Null));
            aistop.setDurationTrigger(new UnsignedInteger(0));
            aispec.setAISpecStopTrigger(aistop);

            // set up any override configuration.  none in this case
            InventoryParameterSpec ispec = new InventoryParameterSpec();
            ispec.setAntennaConfigurationList(null);
            ispec.setInventoryParameterSpecID(new UnsignedShort(23));
            ispec.setProtocolID(new AirProtocols(AirProtocols.EPCGlobalClass1Gen2));
            java.util.List ilist = new java.util.ArrayList();
            ilist.add(ispec);

            aispec.setInventoryParameterSpecList(ilist);
            java.util.List slist = new java.util.ArrayList();
            slist.add(aispec);
            rospec.setSpecParameterList(slist);

            addRoSpec.setROSpec(rospec);

            return addRoSpec;
        }

        private ADD_ROSPEC buildROSpecFromFile()
        {
            logger.info("Loading ADD_ROSPEC message from file ADD_ROSPEC.xml ...");
            try
            {
                LLRPMessage addRospec = Util.loadXMLLLRPMessage(new java.io.File("examples/ADD_ROSPEC.xml"));
                // TODO make sure this is an ADD_ROSPEC message 
                return (ADD_ROSPEC)addRospec;
            }
            catch (java.io.FileNotFoundException)
            {
                logger.error("Could not find file");
                Application.Exit();
            }
            catch (java.io.IOException)
            {
                logger.error("IO Exception on file");
                Application.Exit();
            }
            catch (JDOMException)
            {
                logger.error("Unable to convert LTK-XML to DOM");
                Application.Exit();
            }
            catch (InvalidLLRPMessageException)
            {
                logger.error("Unable to convert LTK-XML to Internal Object");
                Application.Exit();
            }
            return null;
        }

        public LLRPMessage loadXMLLLRPMessage(java.io.File file)
        {
            Document doc = (new SAXBuilder()).build(new FileReader(file));
            XMLOutputter outputter = new XMLOutputter(Format.getPrettyFormat());
            logger.debug("Loaded XML Message: " + outputter.outputString(doc));
            LLRPMessage message = LLRPMessageFactory.createLLRPMessage(doc);
            return message;
        }

        private void setReaderConfiguration()
        {
            LLRPMessage response;

            logger.info("Loading SET_READER_CONFIG message from file SET_READER_CONFIG.xml ...");

            try
            {
                //LLRPMessage setConfigMsg = UtilloadXMLLLRPMessage(new File("examples/SET_READER_CONFIG.xml"));
                LLRPMessage setConfigMsg = loadXMLLLRPMessage(new java.io.File("examples/SET_READER_CONFIG.xml"));
                // TODO make sure this is an SET_READER_CONFIG message

                response = connection.transact(setConfigMsg, 10000);

                // check whetherSET_READER_CONFIG addition was successful
                StatusCode status = ((SET_READER_CONFIG_RESPONSE)response).getLLRPStatus().getStatusCode();
                if (status.equals(new StatusCode("M_Success")))
                {
                    logger.info("SET_READER_CONFIG was successful");
                }
                else
                {
                    logger.info(response.toXMLString());
                    logger.info("SET_READER_CONFIG failures");
                    Application.Exit();
                }

            }
            catch (TimeoutException)
            {
                logger.error("Timeout waiting for SET_READER_CONFIG response");
                Application.Exit();
            }
            catch (java.io.FileNotFoundException)
            {
                logger.error("Could not find file");
                Application.Exit();
            }
            catch (java.io.IOException)
            {
                logger.error("IO Exception on file");
                Application.Exit();
            }
            catch (JDOMException)
            {
                logger.error("Unable to convert LTK-XML to DOM");
                Application.Exit();
            }
            catch (InvalidLLRPMessageException)
            {
                logger.error("Unable to convert LTK-XML to Internal Object");
                Application.Exit();
            }
        }

        private void addRoSpec(bool xml)
        {
            LLRPMessage response;

            ADD_ROSPEC addRospec = null;

            if (xml)
            {
                addRospec = buildROSpecFromFile();
            }
            else
            {
                addRospec = buildROSpecFromObjects();
            }

            addRospec.setMessageID(getUniqueMessageID());
            rospec = addRospec.getROSpec();

            logger.info("Sending ADD_ROSPEC message  ...");
            try
            {
                response = connection.transact(addRospec, 10000);

                // check whether ROSpec addition was successful
                StatusCode status = ((ADD_ROSPEC_RESPONSE)response).getLLRPStatus().getStatusCode();
                if (status.equals(new StatusCode("M_Success")))
                {
                    logger.info("ADD_ROSPEC was successful");
                }
                else
                {
                    logger.info(response.toXMLString());
                    logger.info("ADD_ROSPEC failures");
                    Application.Exit();
                }
            }
            catch (InvalidLLRPMessageException)
            {
                logger.error("Could not display response string");
            }
            catch (TimeoutException)
            {
                logger.error("Timeout waiting for ADD_ROSPEC response");
                Application.Exit();
            }
        }

        private void enable()
        {
            LLRPMessage response;
            try
            {
                // factory default the reader
                logger.info("ENABLE_ROSPEC ...");
                ENABLE_ROSPEC ena = new ENABLE_ROSPEC();
                ena.setMessageID(getUniqueMessageID());
                ena.setROSpecID(rospec.getROSpecID());

                response = connection.transact(ena, 10000);

                // check whether ROSpec addition was successful
                StatusCode status = ((ENABLE_ROSPEC_RESPONSE)response).getLLRPStatus().getStatusCode();
                if (status.equals(new StatusCode("M_Success")))
                {
                    logger.info("ENABLE_ROSPEC was successful");
                }
                else
                {
                    logger.error(response.toXMLString());
                    logger.info("ENABLE_ROSPEC_RESPONSE failed ");
                    Application.Exit();
                }
            }
            catch (java.lang.Exception e)
            {
                e.printStackTrace();
                Application.Exit();
            }
        }

        private void start()
        {
            LLRPMessage response;
            try
            {
                logger.info("START_ROSPEC ...");
                START_ROSPEC start = new START_ROSPEC();
                start.setMessageID(getUniqueMessageID());
                start.setROSpecID(rospec.getROSpecID());

                response = connection.transact(start, 10000);

                // check whether ROSpec addition was successful
                StatusCode status = ((START_ROSPEC_RESPONSE)response).getLLRPStatus().getStatusCode();
                if (status.equals(new StatusCode("M_Success")))
                {
                    logger.info("START_ROSPEC was successful");
                }
                else
                {
                    logger.error(response.toXMLString());
                    logger.info("START_ROSPEC_RESPONSE failed ");
                    Application.Exit();
                }
            }
            catch (java.lang.Exception e)
            {
                e.printStackTrace();
                Application.Exit();
            }
        }

        public void stop()
        {
            LLRPMessage response;
            try
            {
                logger.info("STOP_ROSPEC ...");
                STOP_ROSPEC stop = new STOP_ROSPEC();
                stop.setMessageID(getUniqueMessageID());
                stop.setROSpecID(rospec.getROSpecID());

                response = connection.transact(stop, 10000);

                // check whether ROSpec addition was successful
                StatusCode status = ((STOP_ROSPEC_RESPONSE)response).getLLRPStatus().getStatusCode();
                if (status.equals(new StatusCode("M_Success")))
                {
                    logger.info("STOP_ROSPEC was successful");
                }
                else
                {
                    logger.error(response.toXMLString());
                    logger.info("STOP_ROSPEC_RESPONSE failed ");
                    Application.Exit();
                }
            }
            catch (java.lang.Exception e)
            {
                e.printStackTrace();
                Application.Exit();
            }
        }

        private void logOneCustom(Custom cust)
        {
            if (!cust.getVendorIdentifier().equals(25882))
            {
                logger.error("Non Impinj Extension Found in message");
                return;
            }
        }

        string calculateVelocity()
        {
            string out1 = " Velocity: Unknown";
            double velocity = 0;

            /* you have to have two samples from the same EPC on the same
             * antenna and channel.  NOTE: this is just a simple example.
             * More sophisticated apps would create a database with
             * this information per-EPC */
            if ((lastEPCData != null) &&
                (lastEPCData.Equals(currentEPCData)) &&
                (lastAntennaID.equals(currentAntennaID)) &&
                (lastChannelIndex.equals(currentChannelIndex)) &&
                (lastReadTime.toLong() < currentReadTime.toLong()))
            {
                /* positive velocity is moving towards the antenna */
                double phaseChangeDegrees = (((double)currentRfPhase - (double)lastRfPhase) * 360.0) / 4096.0;
                double timeChangeUsec = (currentReadTime.intValue() - lastReadTime.intValue());

                /* always wrap the phase to between -180 and 180 */
                while (phaseChangeDegrees < -180)
                    phaseChangeDegrees += 360;
                while (phaseChangeDegrees > 180)
                    phaseChangeDegrees -= 360;

                /* if our phase changes close to 180 degrees, you can see we
                ** have an ambiguity of whether the phase advanced or retarded by
                ** 180 degrees (or slightly over). There is no way to tell unless
                ** you use more advanced techiques with multiple channels.  So just
                ** ignore any samples where phase change is > 90 */
                if (java.lang.Math.abs((int)phaseChangeDegrees) <= 90)
                {
                    /* We can divide these two to get degrees/usec, but it would be more
                    ** convenient to have this in a common unit like meters/second.
                    ** Here's a straightforward conversion.  NOTE: to be exact here, we
                    ** should use the channel index to find the channel frequency/wavelength.
                    ** For now, I'll just assume the wavelength corresponds to mid-band at
                    ** 0.32786885245901635 meters. The formula below eports meters per second.
                    ** Note that 360 degrees equals only 1/2 a wavelength of motion because
                    ** we are computing the round trip phase change.
                    **
                    **  phaseChange (degrees)   1/2 wavelength     0.327 meter      1000000 usec
                    **  --------------------- * -------------- * ---------------- * ------------
                    **  timeChange (usec)       360 degrees       1  wavelength      1 second
                    **
                    ** which should net out to estimated tag velocity in meters/second */

                    velocity = ((phaseChangeDegrees * 0.5 * 0.327868852 * 1000000) / (360 * timeChangeUsec));

                    out1 = " VelocityEstimate: " +
                            velocity;
                }
            }

            // save the current sample as the alst sample
            lastReadTime = currentReadTime;
            lastEPCData = currentEPCData;
            lastRfPhase = currentRfPhase;
            lastAntennaID = currentAntennaID;
            lastChannelIndex = currentChannelIndex;
            return out1;
        }
        #region 更新数据
        /// <summary>
        /// 委托
        /// </summary>
        public delegate void ReadDataHandler(List<RFIDData> args);
        /// <summary>
        /// 事件
        /// </summary>
        public event ReadDataHandler ReadData;
        #endregion
        private void logOneTagReport(TagReportData tr)
        {
            // As an example here, we'll just get the stuff out of here and
            // for a super long string

            LLRPParameter epcp = (LLRPParameter)tr.getEPCParameter();

            // epc is not optional, so we should fail if we can't find it
            string epcString = "EPC: ";
            string ename = "";

            if (epcp != null)
            {
                if (epcp.getName().Equals("EPC_96"))
                {
                    EPC_96 epc96 = (EPC_96)epcp;
                    epcString += epc96.getEPC().toString();
                    ename = epc96.getEPC().toString();
                    currentEPCData = epc96.getEPC().toString();
                }
                else if (epcp.getName().Equals("EPCData"))
                {
                    EPCData epcData = (EPCData)epcp;
                    epcString += epcData.getEPC().toString();
                    ename = epcData.getEPC().toString();
                    currentEPCData = epcData.getEPC().toString();
                }
            }
            else
            {
                logger.error("Could not find EPC in Tag Report");
                Application.Exit();
            }

            // all of these values are optional, so check their non-nullness first
            if (tr.getAntennaID() != null)
            {
                epcString += " Antenna: " +
                        tr.getAntennaID().getAntennaID().toString();
                currentAntennaID = tr.getAntennaID().getAntennaID();
            }

            if (tr.getChannelIndex() != null)
            {
                epcString += " ChanIndex: " +
                        tr.getChannelIndex().getChannelIndex().toString();
                currentChannelIndex = tr.getChannelIndex().getChannelIndex();
            }

            if (tr.getFirstSeenTimestampUTC() != null)
            {
                epcString += " FirstSeen: " +
                        tr.getFirstSeenTimestampUTC().getMicroseconds().toString();
                currentReadTime = tr.getFirstSeenTimestampUTC().getMicroseconds();
            }

            if (tr.getInventoryParameterSpecID() != null)
            {
                epcString += " ParamSpecID: " +
                        tr.getInventoryParameterSpecID().getInventoryParameterSpecID().toString();
            }

            if (tr.getLastSeenTimestampUTC() != null)
            {
                epcString += " LastTime: " +
                        tr.getLastSeenTimestampUTC().getMicroseconds().toString();
            }

            if (tr.getPeakRSSI() != null)
            {
                epcString += " RSSI: " +
                        tr.getPeakRSSI().getPeakRSSI().toString();
            }

            if (tr.getROSpecID() != null)
            {
                epcString += " ROSpecID: " +
                        tr.getROSpecID().getROSpecID().toString();
            }

            if (tr.getTagSeenCount() != null)
            {
                epcString += " SeenCount: " +
                        tr.getTagSeenCount().getTagCount().toString();
            }

            java.util.List clist = tr.getCustomList();

            for (int i = 0; i < clist.size(); i++)
            {
                Custom cd = (Custom)clist.get(i);
                if (cd is ImpinjRFPhaseAngle)
                {
                    epcString += " ImpinjPhase: " +
                        ((ImpinjRFPhaseAngle)cd).getPhaseAngle().toString();
                    Integer temp = ((ImpinjRFPhaseAngle)cd).getPhaseAngle().toInteger();
                    currentRfPhase = temp.floatValue();
                }
                if (cd is ImpinjPeakRSSI)
                {
                    epcString += " ImpinjPeakRSSI: " +
                            ((ImpinjPeakRSSI)cd).getRSSI().toString();
                    Integer temp = ((ImpinjPeakRSSI)cd).getRSSI().toInteger();
                    currentPeakRSSI = temp.floatValue();
                }

            }

            epcString += calculateVelocity();
            System.Console.WriteLine(epcString);
            phaseCount += 1;
            System.Console.WriteLine("----count ---------" + phaseCount);

            try
            {                              
                RFIDData arg = new();
                java.math.BigInteger s = currentReadTime.toBigInteger();
                if (!begin)
                {
                    beginTime = Convert.ToInt64(s.toString()) / 1000;
                    begin = true;
				}
				arg.Time = Convert.ToInt64(s.toString()) / 1000 - beginTime;
                arg.Tag = ename;
                arg.Phase = currentRfPhase;
                arg.RSSI = currentPeakRSSI;
                arg.Channel = Convert.ToInt32(currentChannelIndex.toInteger().toString());
                //DataProcess.Baseline(arg);
                // 优化为全局变量
                using (var writer = new StreamWriter(System.IO.File.Open(WriteCsvFilePath!, FileMode.Append)))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecord(arg);
                        csv.NextRecord();
                    }
                }
                ReadData(new List<RFIDData>() { arg });
                epcname = ename;
            }
            catch (java.lang.Exception e)
            {
                e.printStackTrace();
            }

            System.Console.WriteLine(epcString);
        }


        // messageReceived method is called whenever a message is received
        // asynchronously on the LLRP connection.
        public void messageReceived(LLRPMessage message)
        {
            // convert all messages received to LTK-XML representation
            // and print them to the console

            logger.debug("Received " + message.getName() + " message asychronously");

            if (message.getTypeNum() == RO_ACCESS_REPORT.TYPENUM)
            {
                RO_ACCESS_REPORT report = (RO_ACCESS_REPORT)message;

                List tdlist = report.getTagReportDataList();

                for (int i = 0; i < tdlist.size(); i++)
                {
                    TagReportData tr = (TagReportData)tdlist.get(i);
                    logOneTagReport(tr);
                }

                List clist = report.getCustomList();
                for (int i = 0; i < clist.size(); i++)
                {
                    Custom cust = (Custom)clist.get(i);
                    logOneCustom(cust);
                }


            }
            else if (message.getTypeNum() == READER_EVENT_NOTIFICATION.TYPENUM)
            {
                // TODO 
            }
        }

        public void errorOccured(string s)
        {
            logger.error(s);
        }


        //开机
        public void powerON(string args, string fileName)
        {
            BasicConfigurator.configure();
			FileInfo csvFile;
			try
			{
				csvFile = new FileInfo(Path.Combine(basePath, fileName));
				DirectoryInfo parent = csvFile.Directory!;
				if (parent != null && !parent.Exists)
				{
					parent.Create();
				}
                if (!System.IO.File.Exists(csvFile.FullName))
                {
					using (csvFile.Create()) { }
					using var writer = new StreamWriter(System.IO.File.Open(csvFile.FullName!, FileMode.Append));
					using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
					csv.WriteHeader<RFIDData>();
					csv.NextRecord();
				}				
			}
			catch (System.IO.IOException e)
			{
				System.Console.WriteLine(e.ToString());
			}
			WriteCsvFilePath = Path.Combine(basePath, fileName);

			// Only show root events from the base logger
			Logger.getRootLogger().setLevel(Level.ERROR);
			
            //csvWriter = new CsvWriter(WRITE_CSV_FILE_PATH, ',', Charset.forName("GBK"));
            //DocSample4 example = new DocSample4();
            logger.setLevel(Level.INFO);
            connect(args);
        }
        //关机
        public void PowerOFF()
        {
            stop();
            disconnect();
        }
        //开始读
        public void startRead(string? filepath)
        {
            if (!string.IsNullOrEmpty(filepath))
            {
				FileInfo csvFile;
				try
				{
					csvFile = new FileInfo(filepath);
					DirectoryInfo parent = csvFile.Directory!;
					if (parent != null && !parent.Exists)
					{
						parent.Create();
					}
                    csvFile.Create();
				}
				catch (System.IO.IOException e)
				{
                    System.Console.WriteLine(e.ToString());
				}
                WriteCsvFilePath = filepath;
			}
            enableImpinjExtensions();
            factoryDefault();
            getReaderCapabilities();
            getReaderConfiguration();
            setReaderConfiguration();
            addRoSpec(true);
            enable();
            start();
            try
            {
                java.lang.Thread.sleep(60000);
            }
            catch (InterruptedException)
            {
                logger.error("Sleep Interrupted");
            }
        }
    }
    /// <summary>
    /// RFID 数据
    /// </summary>
    public class RFIDData : EventArgs
    {
        public int? Index { get; set; } = null;
        public long? Time { get; set; }
        public string? Tag { get; set; }
        public double Phase { get; set; }
        public double? ProcessedPhase { get; set; } = null;
        public int Channel { get; set; }
        public double RSSI { get; set; }
    }

    public class CSVRFIDData
    {
        
    }
}
