(ns rsteam.angle-test
  (:require [cljs.test :refer-macros [deftest is testing]]
            [rsteam.math.angle :as a]
            [rsteam.math.utils :refer [close?]]))

(deftest degrees-to-radians
  (is (= Math/PI
         (a/degrees-to-radians 180))))

(deftest equality
  (is (= (a/degrees 0)
         (a/degrees 360)))
  (is (= (a/degrees 180)
         (a/degrees -180)))
  (is (= (a/degrees -90)
         (a/degrees 270))))

(deftest opposite
  (is (= (a/degrees -90)
         (a/opposite (a/degrees 90))))
  (is (= (a/degrees 270)
         (a/opposite (a/degrees 90)))))

(deftest dist
  (testing "Without explicit mode it should choose the min"
    (is (close? (a/degrees 0)
                (a/dist (a/degrees 0)
                        (a/degrees 360))
                0.00001))
    (is (close? (a/degrees 90)
                (a/dist (a/degrees 45)
                        (a/degrees -45))
                0.00001))
    (is (close? (a/degrees 90)
                (a/dist (a/degrees -45)
                        (a/degrees 45))
                0.00001))
    (is (close? (a/degrees 90)
                (a/dist 11.780972450961723
                        -5.497787143782138)
                0.00001)))
  (testing "Mode :clockwise"
    (is (close? (a/degrees 90)
                (a/dist (a/degrees 45)
                        (a/degrees -45)
                        :clockwise)
                0.00001))
    (is (close? (a/degrees 90)
                (a/dist (a/degrees 45)
                        (a/degrees 315)
                        :clockwise)
                0.00001))
    (is (close? (a/degrees 270)
                (a/dist 11.780972450961723
                        -5.497787143782138
                        :clockwise)
                0.00001)))
  (testing "Mode :counterclockwise"
    (is (close? (a/degrees 270)
                (a/dist (a/degrees 45)
                        (a/degrees -45)
                        :counterclockwise)
                0.00001))
    (is (close? (a/degrees 270)
                (a/dist (a/degrees 45)
                        (a/degrees 315)
                        :counterclockwise)
                0.00001))
    (is (close? (a/degrees 90)
                (a/dist 11.780972450961723
                        -5.497787143782138
                        :counterclockwise)
                0.00001)))
  (testing "Mode :invalid"
    (is (thrown? ExceptionInfo
                 (a/dist (a/degrees 45)
                         (a/degrees -45)
                         :invalid)))))
