import crypto from 'crypto'

import rimraf from 'rimraf'
import commonjs from '@rollup/plugin-commonjs'
import resolve from '@rollup/plugin-node-resolve'
import typescript from '@rollup/plugin-typescript'
import livereload from 'rollup-plugin-livereload'
import svelte from 'rollup-plugin-svelte'
import { terser } from 'rollup-plugin-terser'
import sveltePreprocess from 'svelte-preprocess'


const production = !process.env.ROLLUP_WATCH
const distPath = '../Wowthing.Web/wwwroot/dist'

rimraf.sync(`${distPath}/*`)

export default {
    input: 'ts/main.ts',
    output: {
        sourcemap: !production,
        format: 'iife',
        name: 'app',
        dir: distPath,
        entryFileNames: production ? '[name].[hash].js' : '[name].dev.js',
    },
    plugins: [
        svelte({
            // enable run-time checks when not in production
            dev: !production,
            css: (css) => {
                if (production) {
                    const hash = crypto.createHash('md5').update(css.code).digest('hex').substr(0, 8)
                    css.write(`main.${hash}.css`, false)
                }
                else {
                    css.write('main.dev.css', true)
                }
            },
            preprocess: sveltePreprocess({
                //postcss: true,
            }),
        }),
        resolve({
            browser: true,
            dedupe: ['svelte']
        }),
        commonjs(),
        typescript({
            sourceMap: !production,
            inlineSources: !production
        }),

        // Watch the `dist` directory and refresh the
        // browser on changes when not in production
        !production && livereload(distPath),

        // If we're building for production (npm run build
        // instead of npm run dev), minify
        production && terser(),
    ],
    watch: {
        include: ['scss/**/*', 'ts/**/*'],
        chokidar: {
            usePolling: true,
        },
        clearScreen: false
    }
}