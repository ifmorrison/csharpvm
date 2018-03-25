using System;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Compute.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace vmclient
{
    class Program
    {
        static void Main(string[] args)
        {
            var credentials = SdkContext.AzureCredentialsFactory
    .FromFile(Environment.GetEnvironmentVariable("AZURE_AUTH_LOCATION"));

            var azure = Azure
                .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithDefaultSubscription();

            var groupName = "csharpresourcegroup";
            var vmName = "mytestVN12411";
            var location = Region.UKSouth;

            Console.WriteLine("Creating resource group...");

            var resourceGroup = azure.ResourceGroups.Define(groupName);

            var vm = azure.VirtualMachines.GetByResourceGroup(groupName, vmName);

            Console.WriteLine("Getting information about the virtual machine...");
            Console.WriteLine("hardwareProfile");
            Console.WriteLine("   vmSize: " + vm.Size);
            Console.WriteLine("storageProfile");
            Console.WriteLine("  imageReference");
            Console.WriteLine("    publisher: " + vm.StorageProfile.ImageReference.Publisher);
            Console.WriteLine("    offer: " + vm.StorageProfile.ImageReference.Offer);
            Console.WriteLine("    sku: " + vm.StorageProfile.ImageReference.Sku);
            Console.WriteLine("    version: " + vm.StorageProfile.ImageReference.Version);
            Console.WriteLine("  osDisk");
            Console.WriteLine("    osType: " + vm.StorageProfile.OsDisk.OsType);
            Console.WriteLine("    name: " + vm.StorageProfile.OsDisk.Name);
            Console.WriteLine("    createOption: " + vm.StorageProfile.OsDisk.CreateOption);
            Console.WriteLine("    caching: " + vm.StorageProfile.OsDisk.Caching);
            Console.WriteLine("osProfile");
            Console.WriteLine("  computerName: " + vm.OSProfile.ComputerName);
            Console.WriteLine("  adminUsername: " + vm.OSProfile.AdminUsername);
            Console.WriteLine("  provisionVMAgent: " + vm.OSProfile.WindowsConfiguration.ProvisionVMAgent.Value);
            Console.WriteLine("  enableAutomaticUpdates: " + vm.OSProfile.WindowsConfiguration.EnableAutomaticUpdates.Value);
            Console.WriteLine("networkProfile");
            foreach (string nicId in vm.NetworkInterfaceIds)
            {
                Console.WriteLine("  networkInterface id: " + nicId);
            }
            Console.WriteLine("vmAgent");
            Console.WriteLine("  vmAgentVersion" + vm.InstanceView.VmAgent.VmAgentVersion);
            Console.WriteLine("    statuses");
            foreach (InstanceViewStatus stat in vm.InstanceView.VmAgent.Statuses)
            {
                Console.WriteLine("    code: " + stat.Code);
                Console.WriteLine("    level: " + stat.Level);
                Console.WriteLine("    displayStatus: " + stat.DisplayStatus);
                Console.WriteLine("    message: " + stat.Message);
                Console.WriteLine("    time: " + stat.Time);
            }
            Console.WriteLine("disks");
            foreach (DiskInstanceView disk in vm.InstanceView.Disks)
            {
                Console.WriteLine("  name: " + disk.Name);
                Console.WriteLine("  statuses");
                foreach (InstanceViewStatus stat in disk.Statuses)
                {
                    Console.WriteLine("    code: " + stat.Code);
                    Console.WriteLine("    level: " + stat.Level);
                    Console.WriteLine("    displayStatus: " + stat.DisplayStatus);
                    Console.WriteLine("    time: " + stat.Time);
                }
            }
            Console.WriteLine("VM general status");
            Console.WriteLine("  provisioningStatus: " + vm.ProvisioningState);
            Console.WriteLine("  id: " + vm.Id);
            Console.WriteLine("  name: " + vm.Name);
            Console.WriteLine("  type: " + vm.Type);
            Console.WriteLine("  location: " + vm.Region);
            Console.WriteLine("VM instance status");
            foreach (InstanceViewStatus stat in vm.InstanceView.Statuses)
            {
                Console.WriteLine("  code: " + stat.Code);
                Console.WriteLine("  level: " + stat.Level);
                Console.WriteLine("  displayStatus: " + stat.DisplayStatus);
            }
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            //.WithRegion(location)
            //.Create();

            //            Console.WriteLine("Creating availability set...");
            //            var availabilitySet = azure.AvailabilitySets.Define("myAVSet")
            //                .WithRegion(location)
            //                .WithExistingResourceGroup(groupName)
            //                .WithSku(AvailabilitySetSkuTypes.Managed)
            //                .Create();

            //            Console.WriteLine("Creating public IP address...");
            //            var publicIPAddress = azure.PublicIPAddresses.Define("myPublicIP")
            //                .WithRegion(location)
            //                .WithExistingResourceGroup(groupName)
            //                .WithDynamicIP()
            //                .Create();

            //            Console.WriteLine("Creating virtual network...");
            //            var network = azure.Networks.Define("myVNet")
            //                .WithRegion(location)
            //                .WithExistingResourceGroup(groupName)
            //                .WithAddressSpace("10.0.0.0/16")
            //                .WithSubnet("mySubnet", "10.0.0.0/24")
            //                .Create();


            //           Console.WriteLine("Creating network interface...");
            //            var networkInterface = azure.NetworkInterfaces.Define("myNIC")
            //                .WithRegion(location)
            //                .WithExistingResourceGroup(groupName)
            //                .WithExistingPrimaryNetwork(network)
            //                .WithSubnet("mySubnet")
            //                .WithPrimaryPrivateIPAddressDynamic()
            //                .WithExistingPrimaryPublicIPAddress(publicIPAddress)
            //                .Create();

            //            Console.WriteLine("Creating virtual machine...");
            //            azure.VirtualMachines.Define(vmName)
            //                .WithRegion(location)
            //                .WithExistingResourceGroup(groupName)
            //                .WithExistingPrimaryNetworkInterface(networkInterface)
            //                .WithLatestWindowsImage("MicrosoftWindowsServer", "WindowsServer", "2012-R2-Datacenter")
            //                .WithAdminUsername("azureuser")
            //                .WithAdminPassword("Azure12345678")
            //                .WithComputerName(vmName)
            //                .WithExistingAvailabilitySet(availabilitySet)
            //                .WithSize(VirtualMachineSizeTypes.StandardA1)
            //                .Create();


        }
    }
    }