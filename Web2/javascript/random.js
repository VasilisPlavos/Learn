let dev = '';
const followMe = (dev) => {
    console.count('followers');
    return `${dev} is following you`;
}

followMe('John'); // followers: 1
followMe('Karen'); // followers: 2
followMe('Camila'); // followers: 3

// more https://dev.to/razgandeanu/9-extremely-powerful-javascript-hacks-4g3p