# ipw4
Easily remember IPs by mapping words to octets. Inspired by what3words.

You can find the mapping list in mapping.json. There is a DNS service at ipw4.com that resolves these names to IPs, for example:

```mid.nil.nil.abs.ipw4.com``` resolves to `127.0.0.1`

## Examples
```
mid.nil.nil.abs.ipw4.com -> 127.0.0.1
abs.abs.abs.abs.ipw4.com -> 1.1.1.1
```

## Technitium App
This project is a module for [Technitium DNS Server](https://technitium.com/dns/). After installing the module, you can create DNS zones that use IPw4 names to resolve to IPv4 addresses.

### Installation
Fetch the latest release from the [Releases](https://github.com/yourusername/ipw4/releases) page, then upload and install the module on the Apps page in Technitium DNS Server.

### Usage
Create an APP record on the apex or a subdomain in the same manner as the Wild IP App. For example, to be able to resolve ```mid.nil.nil.abs.ipw4.com```, create an APP record for the zone `ipw4.com` with the module `Ipw4`.
