import json

with open("mapping.json", "r") as f:
    mapping = json.load(f)
    
if __name__ == "__main__":
    print("Enter an IP Address:")
    ip_address = input().strip()
    octets = ip_address.split(".")
    if len(octets) != 4:
        print("Invalid IP address format.")
    else:
        try:
            words = [mapping[str(int(octet))].lower() for octet in octets]
            print("Your URL (dot notation):", ".".join(words) + ".ipw4.com")
            print("Your URL (dash notation):", "-".join(words) + ".ipw4.com")
        except KeyError:
            print("One of the octets does not have a corresponding word mapping.")