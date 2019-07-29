const path = require('path');

module.exports = {
    context: path.join(__dirname, 'JS'),
    entry: './index.js',
    output: {
        path: path.join(__dirname, 'wwwroot', 'js'),
        filename: '[name].bundle.js',
        library: 'main',
        libraryTarget: 'umd'
    }
};
