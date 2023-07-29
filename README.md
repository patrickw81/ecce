# ecce 1.0.0
Open Source Project 
Image Preprocess with Emgu.CV and OCR Tesseract for different Tasks. Especially for archives and libraries. 
Text Only / File Cards / Catalogues like adressbooks  

Beta Version

- jpg /tiff /png supported
- Languages: eng, deu, Deu Fraktura UB Mannheim, Other languages can be added easily by copying
  traineddata from Tesseract github 
- Preprocess / Segmenation / Define Areas / Batch Process

Preprocess Options:
- Resize
- Noise Reduction
- Sharpen
- Binarize (Otsu/Adopted/ With defined Parameters
- automatic Horizontal correction
- Remove Black Frame
- Remove Noise (Dots)
- Eliminate Lines
- Stronger or weaker lines

Segmentation
- Block / Line / Defined Parameters / Tesseract
- Sort Segments (in Progress)
- Self defined Areas (Load and Save)

OCR
- Teseract
  [Api to Cloud services] not yet implemented

  Batch Process

Save Output as XML / CSV / Txt [Alto not yet implemented]

Work:
- Proof and Correction mode
- Grouping Segments with KMeans for better Segmentation and Layout recognition
- PDF implementation
- Testing Errors and Bugs
