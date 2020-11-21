import { Writable, writable } from 'svelte/store'

import Character from '../models/character'
import fetch_json from '../utils/fetch-json'


export const error = writable(false)
export const loading = writable(true)
export const data = writable({})

export const fetch = async function() {
    const url = document.getElementById('app').getAttribute('data-user')
    const json = await fetch_json(url)
    if (json === null) {
        error.set(true)
        return
    }

    const temp = JSON.parse(json)
    let characters = temp.characters.map((c) => Object.assign(new Character(), c))
    characters.sort((a, b) => {
        if (a.level != b.level) return b.level - a.level
        return a.name.localeCompare(b.name)
    })
    temp.characters = characters

    data.set(temp)
    loading.set(false)
}

export default {
    error,
    loading,
    data,
    fetch,
}