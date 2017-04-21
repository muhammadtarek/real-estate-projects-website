var webpack = require('webpack');
var glob = require("glob");

module.exports = {
   entry: [].concat(glob.sync('./Scripts/**/*.js')),
   plugins: [
       new webpack.DefinePlugin({
           'process.env.NODE_ENV': JSON.stringify('development')
       })
   ],
    output: {
        path: __dirname,
        filename: './Public/main.bundle.js'
    },
    module: {
        loaders: [
            {
                loader: 'babel-loader',
                query: {
                    presets: ['es2015']
                },
                test: /\.js?$/,
                exclude: /(node_modules | bower_components)/
            }
        ]
    }
};