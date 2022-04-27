(ns rsteam.math.angle)

(def ^:const PI Math/PI)
(def ^:const HALF_PI (/ PI 2))
(def ^:const TAU (* 2 PI))
(def ^:const RADIANS_PER_DEGREE (/ PI 180))

(defn degrees-to-radians [degrees]
  (* degrees RADIANS_PER_DEGREE))

(defn radians-to-degrees [radians]
  (/ radians RADIANS_PER_DEGREE))

(defn- normalize [radians] (mod radians TAU))

(defn degrees [d]
  (normalize (degrees-to-radians d)))

(defn radians [r]
  (normalize r))

(defn opposite [r]
  (normalize (+ r PI)))

(defn- dist-clockwise [a b] (normalize (- a b)))
(defn- dist-counterclockwise [a b] (normalize (- b a)))

(defn dist
  ([a b]
    (min (dist-clockwise a b)
         (dist-counterclockwise a b)))
  ([a b mode]
    (case mode
      :clockwise (dist-clockwise a b)
      :counterclockwise (dist-counterclockwise a b)
      (throw (ex-info "Invalid mode" {:angles [a b]
                                      :mode mode})))))
