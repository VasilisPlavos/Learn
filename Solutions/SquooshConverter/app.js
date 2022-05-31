// import { ImagePool } from '@squoosh/lib';
// import { cpus } from 'os';
// const imagePool = new ImagePool(cpus().length);

import fs from 'fs/promises';
// const file = await fs.readFile('./wa5puyrqqyk.jpg');
// const image = imagePool.ingestImage(file);

// const preprocessOptions = {
//     //When both width and height are specified, the image resized to specified size.
//     resize: {
//       width: 100,
//       height: 50,
//     },
//     /*
//     //When either width or height is specified, the image resized to specified size keeping aspect ratio.
//     resize: {
//       width: 100,
//     }
//     */
//   };
//   await image.preprocess(preprocessOptions);
  
//   const encodeOptions = {
//     mozjpeg: {}, //an empty object means 'use default settings'
//     jxl: {
//       quality: 90,
//     },
//   };
//   const result = await image.encode(encodeOptions);


//   var a =  image.encodedWith.mozjpeg;
//   const rawEncodedImage = a.binary;
// fs.writeFile('./image.jpg', rawEncodedImage);
//   await imagePool.close();

  
// console.log(result);






import { ImagePool } from "@squoosh/lib";

// libSquoosh uses a worker-pool under the hood
// to parallelize all image processing.
const imagePool = new ImagePool();

// Accepts both file paths and Buffers/TypedArrays.
const image = imagePool.ingestImage('./wa5puyrqqyk.jpg');

// Optional.
// await image.preprocess({
//   resize: {
//     enabled: true,
//     width: 128,
//   },
// });

await image.encode({
  // All codecs are initialized with default values
  // that can be individually overwritten.
  mozjpeg: {},
  jxl: { quality: 90 },
});

const { extension, binary } = await image.encodedWith.mozjpeg;
await fs.writeFile(`./output.${extension}`, binary);
// ... same for other encoders ...

await imagePool.close();