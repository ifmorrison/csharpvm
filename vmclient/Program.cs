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

            var resourceGroup = azure.ResourceGroups.Define(groupName)
.WithRegion(location)
.Create();

            Console.WriteLine("Creating availability set...");
            var availabilitySet = azure.AvailabilitySets.Define("myAVSet")
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .WithSku(AvailabilitySetSkuTypes.Managed)
                .Create();

            Console.WriteLine("Creating public IP address...");
            var publicIPAddress = azure.PublicIPAddresses.Define("myPublicIP")
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .WithDynamicIP()
                .Create();

            Console.WriteLine("Creating virtual network...");
            var network = azure.Networks.Define("myVNet")
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .WithAddressSpace("10.0.0.0/16")
                .WithSubnet("mySubnet", "10.0.0.0/24")
                .Create();


           Console.WriteLine("Creating network interface...");
            var networkInterface = azure.NetworkInterfaces.Define("myNIC")
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .WithExistingPrimaryNetwork(network)
                .WithSubnet("mySubnet")
                .WithPrimaryPrivateIPAddressDynamic()
                .WithExistingPrimaryPublicIPAddress(publicIPAddress)
                .Create();

            Console.WriteLine("Creating virtual machine...");
            azure.VirtualMachines.Define(vmName)
                .WithRegion(location)
                .WithExistingResourceGroup(groupName)
                .WithExistingPrimaryNetworkInterface(networkInterface)
                .WithLatestWindowsImage("MicrosoftWindowsServer", "WindowsServer", "2012-R2-Datacenter")
                .WithAdminUsername("azureuser")
                .WithAdminPassword("Azure12345678")
                .WithComputerName(vmName)
                .WithExistingAvailabilitySet(availabilitySet)
                .WithSize(VirtualMachineSizeTypes.Standard_B1s)
                .Create();


        }
    }
    }