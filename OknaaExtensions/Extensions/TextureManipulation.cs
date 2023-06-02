using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace OknaaEXTENSIONS {
    public static class TextureExtensions {
        private static Texture2D _inputImage;
        private static int _tileWidth;
        private static int _tileHeight;
        
        
        /// <summary>
        /// Takes a texture2D and splits it into a list of sprites
        /// </summary>
        /// <param name="inputImage">The image to be split</param>
        /// <param name="tilesCount">The number of tiles/Images to be made using the original image (X: Width, Y:Height)</param>
        /// <param name="outputImages">Output parameters holding the list of images</param>
        public static void SplitTextureIntoSprites(this Texture2D inputImage, Vector2Int tilesCount, out List<Sprite> outputImages) {
            _inputImage = inputImage.isReadable ? inputImage : inputImage.GetReadableCopy();

            
            int tilesCount_Width = tilesCount.x;
            int tilesCount_Height = tilesCount.y;

            var imageWidth = inputImage.width;
            var imageHeight = inputImage.height;

            _tileWidth = imageWidth / tilesCount_Width;
            _tileHeight = imageHeight / tilesCount_Height;
            

            outputImages = new List<Sprite>();
            for (int i = 0; i < tilesCount_Width; i++) {
                for (int j = 0; j < tilesCount_Height; j++) {
                    outputImages.Add(CreateTile(i, j));
                }
            }
        }
        
        private static Sprite CreateTile(int i, int j) {
            var tileTexture = CopyTilePixelsToNewTexture2D();
            var tileSprite = GetTileSpriteFromTexture();
            return tileSprite;

            Texture2D CopyTilePixelsToNewTexture2D() {
                Vector2Int tileDimensions = new Vector2Int(_tileWidth, _tileHeight);
                tileTexture = new Texture2D(tileDimensions.x, tileDimensions.y);
                tileTexture.SetPixels(_inputImage.GetPixels(i * tileDimensions.x, j * tileDimensions.y, tileDimensions.x, tileDimensions.y));
                tileTexture.Apply();
                return tileTexture;
            }

            Sprite GetTileSpriteFromTexture() {
                var tileRect = new Rect(0, 0, tileTexture.width, tileTexture.height);
                return Sprite.Create(tileTexture, tileRect, Vector2.one * 0.5f);
            }
        }
        
        
        /// <summary>
        /// Duplicates the source texture, and returns a readable copy, same as checking the Read/Write box in the editor.
        /// Use this when you have to access a new texture pixels at runtime, and you cant enable the Read/Write access beforehand. 
        /// </summary>
        public static Texture2D GetReadableCopy(this Texture2D source) {
            RenderTexture newRenderTexture = RenderTexture.GetTemporary(
                source.width,
                source.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

            Graphics.Blit(source, newRenderTexture);
            RenderTexture previousRenderTexture = RenderTexture.active;
            RenderTexture.active = newRenderTexture;

            Texture2D readableTexture = new Texture2D(source.width, source.height);
            readableTexture.ReadPixels(new Rect(0, 0, newRenderTexture.width, newRenderTexture.height), 0, 0);
            readableTexture.Apply();
            RenderTexture.active = previousRenderTexture;
            RenderTexture.ReleaseTemporary(newRenderTexture);
            return readableTexture;
        }
        
        /// <summary>
        /// Gets the Rect of a Texture2D
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public static Rect Rect(this Texture2D sprite) => new Rect(0, 0, sprite.width, sprite.height);
    }
}