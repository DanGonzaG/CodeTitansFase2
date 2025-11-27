
// Crypto E2EE Helper v1.0
 // AES-GCM + RSA-OAEP-256


const CryptoE2EE = (() => {


    function toBase64(bytes) {
        return btoa(String.fromCharCode.apply(null, new Uint8Array(bytes)));
    }
    function fromBase64(b64) {
        return new Uint8Array([...atob(b64)].map(ch => ch.charCodeAt(0)));
    }
    function ab2str(buf) {
        return String.fromCharCode.apply(null, new Uint8Array(buf));
    }

     //AES (archivo)
    async function encryptAESFile(fileBytes) {
        const aesKey = await window.crypto.subtle.generateKey(
            { name: "AES-GCM", length: 256 },
            true,
            ["encrypt", "decrypt"]
        );
        const rawAES = await window.crypto.subtle.exportKey("raw", aesKey);

        const iv = window.crypto.getRandomValues(new Uint8Array(12));

        const encrypted = await window.crypto.subtle.encrypt(
            { name: "AES-GCM", iv: iv },
            aesKey,
            fileBytes
        );

        return {
            ciphertext: new Uint8Array(encrypted),
            aesKeyRaw: new Uint8Array(rawAES),
            ivBase64: toBase64(iv)
        };
    }


     //RSA (Claves Usuario)
    async function generateRSAKeys() {
        const keys = await window.crypto.subtle.generateKey(
            {
                name: "RSA-OAEP",
                modulusLength: 2048,
                publicExponent: new Uint8Array([1, 0, 1]),
                hash: "SHA-256"
            },
            true,
            ["encrypt", "decrypt"]
        );

        const publicKeyExport = await window.crypto.subtle.exportKey("spki", keys.publicKey);
        const privateKeyExport = await window.crypto.subtle.exportKey("pkcs8", keys.privateKey);

        return {
            publicKeyPem: toBase64(publicKeyExport),
            privateKeyRaw: privateKeyExport
        };
    }


     //Guardar clave privada en cookie cifrada
    function savePrivateKeyCookie(privateKeyRaw) {
        const encoded = toBase64(privateKeyRaw);
        document.cookie = `privKey=${encoded}; max-age=31536000; Secure; SameSite=Strict`;
    }

    function getPrivateKeyCookie() {
        const match = document.cookie.match(new RegExp('(^| )privKey=([^;]+)'));
        return match ? fromBase64(match[2]) : null;
    }


     //Importar clave privada para usar
    async function importPrivateKeyForUse(raw) {
        return await window.crypto.subtle.importKey(
            "pkcs8",
            raw,
            { name: "RSA-OAEP", hash: "SHA-256" },
            false,
            ["decrypt"]
        );
    }


     //Enviar clave pública al backend
    async function sendPublicKeyToServer(publicKeyBase64) {
        return await fetch("/DocumentosCita/GuardarClavePublicaAutomatica", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ publicKeyBase64 })
        });
    }


     //Cifrar AES con RSA pública
    async function encryptAESKeyWithRSA(aesRawKey, publicKeyBase64) {
        const pubKey = await window.crypto.subtle.importKey(
            "spki",
            fromBase64(publicKeyBase64),
            { name: "RSA-OAEP", hash: "SHA-256" },
            false,
            ["encrypt"]
        );
        const encryptedKey = await window.crypto.subtle.encrypt(
            { name: "RSA-OAEP" },
            pubKey,
            aesRawKey
        );
        return toBase64(new Uint8Array(encryptedKey));
    }


     // Descifrar clave AES con cookie privada
    async function decryptAESKeyRSA(encryptedKeyBase64) {
        const raw = getPrivateKeyCookie();
        if (!raw) throw new Error("⚠ La clave privada no está disponible (vacías cookies)");

        const privKey = await importPrivateKeyForUse(raw);

        const decrypted = await window.crypto.subtle.decrypt(
            { name: "RSA-OAEP" },
            privKey,
            fromBase64(encryptedKeyBase64)
        );

        return new Uint8Array(decrypted);
    }
    // E2EE: Gestión de claves RSA 
    const CryptoE2EE = {

        async ensureKeyPair() {
            // Si ya hay clave privada guardada, no hacer nada
            if (this.getPrivateKeyCookie()) return;

            console.warn(" Generando par RSA para el usuario...");

            // Generar RSA 2048
            const keyPair = await crypto.subtle.generateKey(
                {
                    name: "RSA-OAEP",
                    modulusLength: 2048,
                    publicExponent: new Uint8Array([1, 0, 1]),
                    hash: "SHA-256"
                },
                true,
                ["encrypt", "decrypt"]
            );

            // Exportar clave privada PKCS8
            const privateKeyPkcs8 = await crypto.subtle.exportKey("pkcs8", keyPair.privateKey);
            const privateKeyRaw = new Uint8Array(privateKeyPkcs8);
            const privateB64 = btoa(String.fromCharCode(...privateKeyRaw));

            // Guardar clave privada en cookie segura SAME SITE STRONG
            document.cookie = `PK_RSA=${privateB64}; path=/; secure; samesite=strict`;

            // Exportar clave pública SPKI
            const publicKeySpki = await crypto.subtle.exportKey("spki", keyPair.publicKey);
            const publicKeyBase64 = btoa(String.fromCharCode(...new Uint8Array(publicKeySpki)));

            // Enviar clave pública al backend
            await fetch("/DocumentosCita/GuardarClavePublicaAutomatica", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ publicKeyBase64 })
            });

            console.log("🔐 Par RSA generado y clave pública guardada en BD.");
        },

        getPrivateKeyCookie() {
            const match = document.cookie.match(/PK_RSA=([^;]+)/);
            return match ? match[1] : null;
        }
    };


     //Descifrar archivo con AES

    async function decryptFileAES(cipherBase64, ivBase64, aesBytes) {
        const iv = fromBase64(ivBase64);
        const cipherBytes = fromBase64(cipherBase64);

        const aesKey = await window.crypto.subtle.importKey(
            "raw",
            aesBytes,
            { name: "AES-GCM", length: 256 },
            false,
            ["decrypt"]
        );

        const decrypted = await window.crypto.subtle.decrypt(
            { name: "AES-GCM", iv: iv },
            aesKey,
            cipherBytes
        );

        return new Uint8Array(decrypted);
    }
 
// Generar par RSA y guardar localmente

    async function generateKeysAndStore() {

        // 1) Crear claves
        const { publicKeyPem, privateKeyRaw } = await generateRSAKeys();

        // 2) Guardar privada en cookie
        savePrivateKeyCookie(privateKeyRaw);

        await sendPublicKeyToServer(publicKeyPem);

        // 3) Retornar clave pública para guardar en BD
        return publicKeyPem;
    }


     //Asegurar que el usuario tenga clave privada

    async function ensurePrivateKey() {

        const privateRaw = getPrivateKeyCookie();
        if (privateRaw) return true;

        const newPublicBase64 = await generateKeysAndStore();

        // Enviar al backend
        await sendPublicKeyToServer(newPublicBase64);

        console.log("🔐 Clave privada generada automaticamente y clave pública guardada");
        return true;
    }

    return {
        encryptAESFile,
        generateRSAKeys,
        savePrivateKeyCookie,
        getPrivateKeyCookie,
        sendPublicKeyToServer,
        encryptAESKeyWithRSA,
        decryptAESKeyRSA,
        decryptFileAES,
        ensurePrivateKey,      
        generateKeysAndStore   
    };
})();
