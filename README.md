# wcfperformancecomparison
WCF bindings and configurations performance comparison

Prerequisites

Port access required. Use ‘Run as Administrator’ or add a url reservation using ‘netsh http add urlacl url=http://+:6766/ user=MYMACHINE/MYACCOUNT’. Mind the tailing slash in the url, and scheme (http or https)
For Https bindings, a certificate is required. Localhost's is OK. It can be set up by 'netsh http add sslcert ipport=0.0.0.0:7866 certhash=<cert thumbprint> appid={<server assembly guid>} certstorename=MY'
