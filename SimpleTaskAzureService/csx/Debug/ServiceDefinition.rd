<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="SimpleTaskAzureService" generation="1" functional="0" release="0" Id="b1c90605-569a-4fca-aebf-31de0b6a90aa" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="SimpleTaskAzureServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="SimpleTaskApp:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/LB:SimpleTaskApp:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/LB:SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Certificate|SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/MapCertificate|SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
        <aCS name="SimpleTaskApp:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/MapSimpleTaskApp:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/MapSimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/MapSimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/MapSimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/MapSimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/MapSimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="SimpleTaskAppInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/MapSimpleTaskAppInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:SimpleTaskApp:Endpoint1">
          <toPorts>
            <inPortMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskApp/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskApp/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskApp/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapCertificate|SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskApp/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
        <map name="MapSimpleTaskApp:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskApp/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapSimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskApp/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapSimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskApp/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapSimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskApp/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapSimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskApp/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapSimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskApp/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapSimpleTaskAppInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskAppInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="SimpleTaskApp" generation="1" functional="0" release="0" software="C:\PROG\DAC\SimpleTask\SimpleTaskAzureService\csx\Debug\roles\SimpleTaskApp" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SW:SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;SimpleTaskApp&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;SimpleTaskApp&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskApp/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskAppInstances" />
            <sCSPolicyUpdateDomainMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskAppUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskAppFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="SimpleTaskAppUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="SimpleTaskAppFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="SimpleTaskAppInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="6d696adb-2df0-4f09-961d-55ceccc3ebf4" ref="Microsoft.RedDog.Contract\ServiceContract\SimpleTaskAzureServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="99f34e90-ea06-478f-b963-59d745c7a7e1" ref="Microsoft.RedDog.Contract\Interface\SimpleTaskApp:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskApp:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="9b2782b4-3593-4464-a5f9-f240bd60fa89" ref="Microsoft.RedDog.Contract\Interface\SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/SimpleTaskAzureService/SimpleTaskAzureServiceGroup/SimpleTaskApp:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>