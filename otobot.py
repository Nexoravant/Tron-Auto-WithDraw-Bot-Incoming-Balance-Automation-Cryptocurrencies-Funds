from github import Github
import os


token = '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'

organization_name = 'Nexoravant'
repo_name = 'Tron-Auto-WithDraw-Bot-Incoming-Balance-Automation-Cryptocurrencies-Funds'
repo_description = 'Tron-based auto-withdraw bot that monitors incoming balances and automates fund transfers. Designed for efficient cryptocurrency management, ensuring instant handling of deposits. Ideal for platforms needing fast, secure, and hands-free fund operations.'

g = Github(token)
org = g.get_organization(organization_name)

try:
    repo = org.create_repo(repo_name, description=repo_description, private=True)
    print(f"'{repo_name}' oluşturuldu.")

    local_dir = r'D:\GithubNew\16\AutoWithDraw\Tron'
    github_dir = r'D:\GithubNew\Github'
    excluded_folders = {'.vscode', 'bin'}

    # UPLOAD
    for root, dirs, files in os.walk(local_dir):
        for filename in files:
            file_path = os.path.join(root, filename)
            relative_path = os.path.relpath(file_path, local_dir)
            
            skip = False
            for folder in excluded_folders:
                if folder in relative_path.split(os.path.sep):
                    skip = True
                    break
            
            if not skip:
                with open(file_path, 'rb') as file:
                    content = file.read()
                    try:
                        repo.create_file(relative_path.replace('\\', '/'), f"{filename}", content.decode('utf-8'))
                        print(f"'{filename}' yüklendi.")
                    except UnicodeDecodeError:
                        repo.create_file(relative_path.replace('\\', '/'), f"{filename}", content)
                        print(f"'{filename}' yüklendi. (binary)")
    
    #          
    additional_topics = [
        'ethereum',              # 1
        'auto-withdraw-bot',          # 2
        'wallet',          # 3
        'automation',     # 4
        'hardware-wallet',    # 5
        'cold-wallet',                  # 6
        'blockchain',              # 7
        'react-native',          # 8
        'bitecoin',          # 9
        'withdraw-bot',          # 10
        'cryptography',            # 11
        'ethereum',     # 12
        'cryptography',              # 13
        'auto-withdraw-script',         # 14
        'withdraw-crypto-automation',              # 15
        'tron',                 # 16
        'evm-chains',            # 17
        'cryptocurrency',            # 18
        'withdrawal',            # 19
        'crypto-bot',            # 20
    ]
    topics = repo.get_topics()
    
    for topic in additional_topics:
        if topic not in topics:
            topics.append(topic)
            print(f"TAGLAR EKLENDI")
    
    repo.replace_topics(topics)

    # LICENSE
    licensee = """MIT License

    Copyright (c) 2024 MIT

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE."""
    
    repo.create_file('LICENSE', 'LICENSE', licensee)
    print("License oluşturuldu.")


    # SECURITY
    security = """# Security Policy

    ## Supported Versions

    Use this section to tell people about which versions of your project are
    currently being supported with security updates.

    | Version | Supported          |
    | ------- | ------------------ |
    | 5.1.x   | :white_check_mark: |
    | 5.0.x   | :x:                |
    | 4.0.x   | :white_check_mark: |
    | < 4.0   | :x:                |

    ## Reporting a Vulnerability

    Use this section to tell people how to report a vulnerability.

    Tell them where to go, how often they can expect to get an update on a
    reported vulnerability, what to expect if the vulnerability is accepted or
    declined, etc.
    """
    
    repo.create_file('SECURITY.md', 'SECURITY', security)
    print("Security oluşturuldu.")
    
    
    tokenv2 = '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'
    gv2 = Github(tokenv2)
    repov2 = gv2.get_repo(f"{organization_name}/{repo_name}")
    github_path = os.path.join(github_dir, '.github')
    
    if os.path.exists(github_path) and os.path.isdir(github_path):
        for root, dirs, files in os.walk(github_path):
            for filename in files:
                file_path = os.path.join(root, filename)
                relative_path = os.path.relpath(file_path, github_dir)
                with open(file_path, 'rb') as file:
                    content = file.read()
                    try:
                        repov2.create_file(relative_path.replace('\\', '/'), f".github", content.decode('utf-8'))
                        print(f"'{filename}' yüklendi.")
                    except UnicodeDecodeError:
                        repov2.create_file(relative_path.replace('\\', '/'), f".github", content)
                        print(f"'{filename}' yüklendi. (binary)")
                    
    # Release
    release_title = 'sandbox'
    release_body = 'sandbox online'
    repo.create_git_release(tag=release_title, name=release_title, message=release_body)
    print(f"release oluşturuldu.")

    # repo.edit(private=False)
    # print("Public Done!")

except Exception as e:
    print(f"Hata oluştu: {e}")

