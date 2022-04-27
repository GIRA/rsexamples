(ns rsteam.math.rectangle)

(defn rectangle [& {:keys [origin corner]}]
  {:origin origin
   :corner corner})

(defn grow-by
  ([rect n] (grow-by rect n n))
  ([rect x y]
   (-> rect
       (update-in [:origin :x] - x)
       (update-in [:origin :y] - y)
       (update-in [:corner :x] + x)
       (update-in [:corner :y] + y))))

(defn shrink-by 
  ([rect n] (shrink-by rect n n))
  ([rect x y]
   (-> rect
       (update-in [:origin :x] + x)
       (update-in [:origin :y] + y)
       (update-in [:corner :x] - x)
       (update-in [:corner :y] - y))))

(defn contains-point? [rect point]
  (let [{:keys [x y]} point
        {x0 :x y0 :y} (:origin rect)
        {x1 :x y1 :y} (:corner rect)]
    (and (< x0 x) (< y0 y)
         (> x1 x) (> y1 y))))