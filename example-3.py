BASE = 1628
with open("mapping-3.txt", 'r') as f:
    WORDS = [line.strip() for line in f.readlines()]

def bin_to_base(n: int) -> tuple[int, int, int]:
    """Convert a 32-bit integer to three numbers in [0, BASE-1]."""
    if not (0 <= n <= 4294967295):
        raise ValueError("Number out of range (0 to 4294967295)")
    a = n // (BASE ** 2)
    b = (n // BASE) % BASE
    c = n % BASE
    return a, b, c

def base_to_bin(a: int, b: int, c: int) -> int:
    """Convert three numbers back to the original integer."""
    if not (0 <= a < BASE and 0 <= b < BASE and 0 <= c < BASE):
        raise ValueError(f"Values must be in range 0 to {BASE-1}")
    return a * (BASE ** 2) + b * BASE + c

def base_to_words(a: int, b: int, c: int) -> tuple[str, str, str]:
    """Convert three numbers to their corresponding words."""
    return WORDS[a], WORDS[b], WORDS[c]

def words_to_base(word1: str, word2: str, word3: str) -> tuple[int, int, int]:
    """Convert three words back to their corresponding numbers."""
    try:
        a = WORDS.index(word1)
        b = WORDS.index(word2)
        c = WORDS.index(word3)
    except ValueError as e:
        raise ValueError("One of the words is not in the mapping.") from e
    return a, b, c

def ip_to_bin(ip: str) -> int:
    """Convert IPv4 address to a 32-bit integer."""
    parts = ip.split('.')
    if len(parts) != 4:
        raise ValueError("Invalid IPv4 address format")
    return (int(parts[0]) << 24) | (int(parts[1]) << 16) | (int(parts[2]) << 8) | int(parts[3])

def bin_to_ip(n: int) -> str:
    """Convert a 32-bit integer back to IPv4 address."""
    if not (0 <= n <= 4294967295):
        raise ValueError("Number out of range for IPv4")
    return f"{(n >> 24) & 255}.{(n >> 16) & 255}.{(n >> 8) & 255}.{n & 255}"

def ip_to_words(ip: str) -> tuple[str, str, str]:
    """Convert an IPv4 address to its corresponding three words."""
    n = ip_to_bin(ip)
    a, b, c = bin_to_base(n)
    return base_to_words(a, b, c)

def words_to_ip(word1: str, word2: str, word3: str) -> str:
    """Convert three words back to their corresponding IPv4 address."""
    a, b, c = words_to_base(word1, word2, word3)
    n = base_to_bin(a, b, c)
    return bin_to_ip(n)




# def encode_to_three(n: int) -> tuple[int, int, int]:
#     """Convert a 32-bit integer to three numbers in [0, BASE-1]."""
#     if not (0 <= n <= 4294967295):
#         raise ValueError("Number out of range (0 to 4294967295)")
#     a = n // (BASE ** 2)
#     b = (n // BASE) % BASE
#     c = n % BASE
#     return a, b, c

# def decode_from_three(a: int, b: int, c: int) -> int:
#     """Convert three numbers back to the original integer."""
#     if not (0 <= a < BASE and 0 <= b < BASE and 0 <= c < BASE):
#         raise ValueError(f"Values must be in range 0 to {BASE-1}")
#     return a * (BASE ** 2) + b * BASE + c

def ip_to_int(ip: str) -> int:
    """Convert IPv4 address to a 32-bit integer."""
    parts = ip.split('.')
    if len(parts) != 4:
        raise ValueError("Invalid IPv4 address format")
    return (int(parts[0]) << 24) | (int(parts[1]) << 16) | (int(parts[2]) << 8) | int(parts[3])

def int_to_ip(n: int) -> str:
    """Convert a 32-bit integer back to IPv4 address."""
    if not (0 <= n <= 4294967295):
        raise ValueError("Number out of range for IPv4")
    return f"{(n >> 24) & 255}.{(n >> 16) & 255}.{(n >> 8) & 255}.{n & 255}"

# ---------------- TEST CASES ----------------
def run_tests():
    print("Running tests...")
    # Edge cases
    cases = [0, 1, BASE-1, BASE, BASE**2, 4294967295]
    for n in cases:
        encoded = encode_to_three(n)
        decoded = decode_from_three(*encoded)
        assert decoded == n, f"Failed for {n}"
        print(f"{n} -> {encoded} -> {decoded}")

    # IP address tests
    ip_cases = ["0.0.0.0", "255.255.255.255", "192.168.0.1", "10.0.0.1"]
    for ip in ip_cases:
        num = ip_to_int(ip)
        back_ip = int_to_ip(num)
        assert back_ip == ip, f"Failed for IP {ip}"
        print(f"{ip} -> {num} -> {back_ip}")

    print("All tests passed!")

if __name__ == "__main__":
    print(ip_to_words("127.0.0.1"))
    print(ip_to_words("1.1.1.1"))
    print(ip_to_words("0.0.0.0"))
    print(ip_to_words("255.255.255.255"))
    
    print(words_to_ip("moral", "shuts", "dusty"))
    print(words_to_ip("will", "curl", "horns"))
    print(words_to_ip("deep", "deep", "deep"))
    print(words_to_ip("canny", "below", "reef"))
    # run_tests()