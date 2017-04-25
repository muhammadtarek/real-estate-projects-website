var webpack = require('webpack');
var glob = require("glob");

module.exports = {
    entry: [].concat(glob.sync('./Scripts/**/*.js')),
    plugins: [
        // Minify assets.
        /*new webpack.optimize.UglifyJsPlugin({
            compress: {
                warnings: false // https://github.com/webpack/webpack/issues/1496
            }
        })*/
    ],
    output: {
        path: __dirname,
        filename: './Public/app.min.js'
    },
    module: {
        loaders: [
            {
                loader: 'babel-loader',
                query: {
                    presets: ['es2015']
                },
                test: /\.jsx?$/,
                exclude: /(node_modules | bower_components)/
            }
        ]
    }
};